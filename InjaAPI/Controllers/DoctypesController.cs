using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InjaDTO;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class DoctypesController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;
	private readonly TokenService _tokenService;

	public DoctypesController(dbContext context, IMapper mapper, TokenService tokenService)
	{
		_context = context;
		_mapper = mapper;
		_tokenService = tokenService;
	}

	// GET: api/Doctypes
	[HttpGet]
	[AllowAnonymous]
	public async Task<ActionResult<IEnumerable<DoctypeDTO>>> GetDoctypes()
	{
		return await _context.Doctypes.ProjectTo<DoctypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

	// GET: api/Doctypes/5
	[HttpGet("{id}")]
	[AllowAnonymous]
	public async Task<ActionResult<DoctypeDTO>> GetDoctype(short id)
	{
		var doctype = await _context.Doctypes.FirstOrDefaultAsync(x => x.Id == id);

		if (doctype == null)
		{
			return NotFound();
		}

		return _mapper.Map<DoctypeDTO>(doctype);
	}
}
