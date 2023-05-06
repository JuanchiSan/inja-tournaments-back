using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using InjaAPI;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Cors;
using InjaDTO;

namespace Inja.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class JudgeController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;
	private readonly TokenService _tokenService;

	public JudgeController(dbContext context, IMapper mapper, TokenService tokenService)
	{
		_context = context;
		_mapper = mapper;
		_tokenService = tokenService;
	}

	
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<List<UsersChallengeCriteriaDTO>>> GetCriteriaByJudge(int eventId, int challengeId, int judgeId, int contenderId)
	{
		try
		{
			var dbResult = await _context.VUserschallengecriteria.Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Judgeid == judgeId && x.Contenderid == contenderId).ToListAsync();

			var dtoResult = _mapper.Map<List<UsersChallengeCriteriaDTO>>(dbResult);

			foreach (var dto in dtoResult) {
				var dbPoint = _context.Points.FirstOrDefault(x => x.Userid == contenderId && x.Eventjudgechallengeid == dto.eventJudgeChallengeDivisionId);
				if (dbPoint != null)
				{
					dto.Point = _mapper.Map<PointDTO>(dbPoint);
				}
			}

			return Ok(dtoResult);
		}
		catch (Exception e)
		{
			Serilog.Log.Error("Internal Error Getting CriteriaByJudge", e);
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}

	[AllowAnonymous]
	[HttpPost("SendPoints")]
	public async Task<ActionResult<PointDTO>> SendPoints(PointDTO aPoint)
	{
		try
		{
			var dbEJCD = await _context.Eventjudgechallengedivisions.FirstOrDefaultAsync(x => x.Id == aPoint.eventJudgeChallengeDivisionId);
			if (dbEJCD == null)
			{
				Serilog.Log.Information("EventJudgeChallengeDivision Not Found");
				return NotFound();
			}

			var dbPoint = await _context.Points.FirstOrDefaultAsync(x => x.Eventjudgechallengeid == aPoint.eventJudgeChallengeDivisionId && x.Userid == aPoint.contenderId);
			if (dbPoint == null)
			{
				dbPoint = _mapper.Map<Point>(aPoint);
				dbPoint.Eventjudgechallenge = null;
				_context.Points.Add(dbPoint);
			}
			else
			{
				_mapper.Map(aPoint, dbPoint);
			}

			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Serilog.Log.Error("Internal Error Getting CriteriaByJudge", e);
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	
		return Ok(aPoint);
	}
}
