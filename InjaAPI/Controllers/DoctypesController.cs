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

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class DoctypesController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;


	public DoctypesController(dbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Doctypes
	[HttpGet]
	public async Task<ActionResult<IEnumerable<DoctypeDTO>>> GetDoctypes()
	{
		return await _context.Doctypes.ProjectTo<DoctypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

	// GET: api/Doctypes/5
	[HttpGet("{id}")]
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
