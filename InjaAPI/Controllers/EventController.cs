using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using InjaDTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Inja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;

	public EventsController(dbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Evetns
	[HttpGet]
	public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents()
	{
		return await _context.Events.ProjectTo<EventDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

		// GET: api/Cities/5
	[HttpGet("{id}")]
	public async Task<ActionResult<EventDTO>> GetCity(int id)
	{
		var city = await _context.Cities.FindAsync(id);

		if (city == null)
		{
			return NotFound();
		}

		return _mapper.Map<EventDTO>(city);
	}

}
