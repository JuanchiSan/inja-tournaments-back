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
public class EventsController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;
	private readonly TokenService _tokenService;
	
	public EventsController(dbContext context, IMapper mapper, TokenService tokenService)
	{
		_context = context;
		_mapper = mapper;
		_tokenService = tokenService;
	}

	[AllowAnonymous]
	[HttpGet("GetEvents")]
	public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents()
	{
		return await _context.Events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

  [AllowAnonymous]
  [HttpGet("{id}")]
	public async Task<ActionResult<EventDTO>> GetEvent(int id)
	{
		var dbItem = await _context.Events.FindAsync(id);

		if (dbItem == null)
		{
			return NotFound();
		}

		return _mapper.Map<EventDTO>(dbItem);
	}

}
