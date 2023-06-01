using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using InjaDTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class CitiesController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;
	private readonly TokenService _tokenService;

	public CitiesController(dbContext context, IMapper mapper, TokenService tokenService)
	{
		_context = context;
		_mapper = mapper;
		_tokenService = tokenService;
	}

	// GET: api/Cities
	[AllowAnonymous]
	[HttpGet]
	public async Task<ActionResult<IEnumerable<CityDTO>>> GetCities()
	{
		return await _context.Cities.ProjectTo<CityDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

	[HttpGet("/api/GetCitiesByName/{aCityName}"), Authorize]
	public async Task<ActionResult<List<CityDTO>>> GetCitiesByName(string aCityName)
	{
		var lstCities = await _context.Cities.ToListAsync();
		lstCities = lstCities.Where(x => x.Name.ToLowerInvariant().Contains(aCityName.ToLowerInvariant())).ToList();

		if (lstCities.Count == 0)
		{
			return NotFound();
		}

		return _mapper.Map<List<CityDTO>>(lstCities);
	}

	// GET: api/Cities/5
	[AllowAnonymous]
	[HttpGet("{id}")]
	public async Task<ActionResult<CityDTO>> GetCity(int id)
	{
		var city = await _context.Cities.FindAsync(id);

		if (city == null)
		{
			return NotFound();
		}

		return _mapper.Map<CityDTO>(city);
	}

}
