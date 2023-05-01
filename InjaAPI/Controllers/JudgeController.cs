using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using InjaDTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace Inja.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class JugdeController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;

	public JugdeController(dbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	//[HttpGet]
	//public async Task<ActionResult<IEnumerable<CityDTO>>> GetCriteriaByJudge(CriteriaByJudgeDTO obj)
	//{
	//	return await _context.Cities.ProjectTo<CityDTO>(_mapper.ConfigurationProvider).ToListAsync();
	//}
}
