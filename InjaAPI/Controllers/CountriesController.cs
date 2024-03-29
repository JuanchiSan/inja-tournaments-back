﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InjaDTO;
using Microsoft.AspNetCore.Authorization;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class CountriesController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;
  
  public CountriesController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  // GET: api/Countries
  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountries()
  {
    return await _context.Countries.ProjectTo<CountryDTO>(_mapper.ConfigurationProvider).ToListAsync();
  }

  // GET: api/Countries/5
  [AllowAnonymous]
  [HttpGet("{id}")]
  public async Task<ActionResult<CountryDTO>> GetCountry(short id)
  {
    var country = await _context.Countries.FirstOrDefaultAsync(x=>x.Id == id);

    if (country == null)
    {
      return NotFound();
    }

    return _mapper.Map<CountryDTO>(country);
  }

  [HttpGet("/api/GetCountriesByName/{aCountyName}")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<CountryDTO>>> GetCountry(string aCountyName)
  {
    var country = await _context.Countries.Include(x => x.Cities).ToListAsync();
    country = country.Where(x => x.Name.ToLowerInvariant().Contains(aCountyName.ToLowerInvariant()))
      .ToList();
    if (country.Count == 0)
    {
      return NotFound();
    }

    return _mapper.Map<List<CountryDTO>>(country);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryDTO)),
   ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string)),
   ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
  public async Task<ActionResult<CountryDTO>> AddCountry(string? aCountryName)
  {
    if (string.IsNullOrEmpty(aCountryName) || string.IsNullOrWhiteSpace(aCountryName))
    {
      return BadRequest("Country Name can't be null or empty");
    }
    
    var result = await Helper.AddCountry(_context, aCountryName);
    return result.Item1 != 500 ? Ok(result.Item2) : StatusCode(result.Item1, "Error Adding new Country");
  }
}
