using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using InjaDTO;

namespace InjaAPI.Controllers;

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

	// [AllowAnonymous]
	// [HttpGet]
	// public async Task<ActionResult<List<UserChallengeCriteriaDTO>>> GetCriteriaByJudge(int eventId, int challengeId, int judgeId, int contenderId)
	// {
	// 	try
	// 	{
	// 		var dbResult = await _context.VUserschallengecriteria
	// 			.Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Judgeid == judgeId && x.Contenderid == contenderId)
	// 			.ToListAsync();
	// 		
	// 		if (!dbResult.Any()) return Ok(new List<UserChallengeCriteriaDTO>()); // No hay elementos
	//
	// 		var cosa = _context.Eventchallenges
	// 			.Include(x=>x.Challenge)
	// 			.Include(ecd=>ecd.Eventchallengedivisions)
	// 				.ThenInclude(ejcd=>ejcd.Eventjudgechallengedivisions)
	// 					.ThenInclude(cjc=>cjc.Challengejuzmentcriteria).ToList();
	// 		
	// 		var dbPoints = _context.Points.Where(x=>x.Userid == contenderId && x.Eventjudgechallengeid)
	// 		
	// 		var cosa2 = from e1 in cosa
	// 		            join e2 in 
	// 		
	// 		var dbPhoto = await _context.Photos
	// 			.Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Contenderid == contenderId)
	// 			.OrderByDescending(y => y.Created)
	// 			.FirstOrDefaultAsync();
	//
	// 		var topItem = new UserChallengeCriteriaDTO
	// 		{
	// 			contenderId = contenderId,
	// 			challengeId = challengeId,
	// 			judgeId = judgeId,
	// 			eventId = eventId,
	// 			challengeEnddate = dbResult.First().ChallengeEnddate,
	// 			challengeStardate = dbResult.First().ChallengeEnddate,
	// 			contenderFirstname = dbResult.First().ContenderFirstname,
	// 			contenderLastname = dbResult.First().ContenderLastname,
	// 			judgeFirstname = dbResult.First().JudgeFirstname,
	// 			judgeLastname = dbResult.First().JudgeLastname,
	// 			photoURL = dbPhoto != null ? $"http://inja-api.guadcore.ar/laphoto" : null
	// 		};
	//
	// 		return Ok(topItem);
	// 		// var dbPoint = await _context.Points
	// 		// 	.Include(z=>z.Eventjudgechallenge)
	// 		// 	.Where(x => x.Userid == contenderId && x.Eventjudgechallenge. Eventjudgechallengeid == dto.eventJudgeChallengeDivisionId);
	//
	// 	}
	// 	catch (Exception e)
	// 	{
	// 		Serilog.Log.Error("Internal Error Getting CriteriaByJudge", e);
	// 		return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
	// 	}
	// }

	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<PhotoUserChallengeCriteriaDTO>> GetCriteriaByJudge(int eventId, int challengeId, int judgeId, int contenderId)
	{
		try
		{
			var dbResult = await _context
				.VUserschallengecriteria
				.Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Judgeid == judgeId && x.Contenderid == contenderId)
				.ToListAsync();

			if (!dbResult.Any()) return NotFound("No Data with those parameters");
			
			var dtoResult = _mapper.Map<List<UserChallengeCriteriaDTO>>(dbResult);

			foreach (var dto in dtoResult) {
			 	var dbPoint = _context.Points.FirstOrDefault(x => x.Userid == contenderId && x.Eventjudgechallengeid == dto.eventJudgeChallengeDivisionId);
			 	if (dbPoint != null)
			 	{
			 		dto.Point = _mapper.Map<PointDTO>(dbPoint);
			 	}
			}
			
			var dbPhoto = await _context.Photos
			 			.Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Contenderid == contenderId)
			 			.OrderByDescending(y => y.Created)
			 			.FirstOrDefaultAsync();  
			
			return Ok(new PhotoUserChallengeCriteriaDTO
			{
				PhotoURL = dbPhoto == null || string.IsNullOrEmpty(dbPhoto.PhotoUrl) ? null : dbPhoto.PhotoUrl,
				UserChallengeCriteria = dtoResult.ToList()
			});
		}
		catch (Exception e)
		{
			Serilog.Log.Error(e,"Internal Error Getting CriteriaByJudge");
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	}

	[AllowAnonymous]
	[HttpPost("SendPoints")]
	public async Task<ActionResult<PointDTO>> SendPoints(PointDTO aPoint)
	{
		try
		{
			var dbEJCD = await _context
				.Eventjudgechallengedivisions
				.FirstOrDefaultAsync(x => x.Id == aPoint.eventJudgeChallengeDivisionId);
			
			if (dbEJCD == null)
			{
				Serilog.Log.Information("EventJudgeChallengeDivision Not Found");
				return NotFound();
			}

			var dbPoint = await _context.Points.FirstOrDefaultAsync(x => x.Eventjudgechallengeid == aPoint.eventJudgeChallengeDivisionId && x.Userid == aPoint.contenderId);
			if (dbPoint == null)
			{
				dbPoint = _mapper.Map<Point>(aPoint);
				dbPoint.Eventjudgechallenge = null!;

				_context.Points.Add(dbPoint);
			}
			else
			{
				for (var i = 1; i <= 10; i++)
				{
					dbPoint.GetType().GetProperty($"Slot{i}")?.SetValue(dbPoint,aPoint.GetType().GetProperty($"Slot{i}")?.GetValue(aPoint));
				}

				dbPoint.Comment = aPoint.Comment;
				//_mapper.Map(aPoint, dbPoint);
			}

			await _context.SaveChangesAsync();
		}
		catch (Exception e)
		{
			Serilog.Log.Error(e,"Internal Error Getting CriteriaByJudge");
			return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
		}
	
		return Ok(aPoint);
	}
}
