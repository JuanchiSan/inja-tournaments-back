﻿using Microsoft.AspNetCore.Mvc;
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

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDTO)),
	 ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string)),
	 ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
	[AllowAnonymous]
	public async Task<ActionResult<CityDTO>> AddCity(string? aCityName, string? aCountryName)
	{
		if (string.IsNullOrEmpty(aCityName) || string.IsNullOrWhiteSpace(aCityName))
		{
			return BadRequest("City Name can't be null or empty");
		}

		var result = await Helper.AddCity(_context, aCityName, aCountryName);
		return result.Item1 != 500 ? Ok(result.Item2) : StatusCode(result.Item1, "Error Adding new City");
	}
}
