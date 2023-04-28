using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using Serilog;
using AutoMapper;
using InjaDTO;
using AutoMapper.QueryableExtensions;

namespace Inja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;

  public UserController(dbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  [HttpGet("/api/GetUsers")]
  public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
  {
    try
    {
      return await _context.Ninjausers.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  // GET: api/UserByEmail/jperez@pepe
  [HttpGet("/api/UserByEmail/{email}")]
  public async Task<ActionResult<UserDTO>> GetPersonByEmail(string email)
  {
    var person =
      await _context.Ninjausers.FirstOrDefaultAsync(x => x.Mail.ToLowerInvariant().Equals(email.ToLowerInvariant()));

    if (person == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(person);
  }

   [HttpGet("{id}")]
  public async Task<ActionResult<UserDTO>> GetUser(int id)
  {
    var dbItem = await _context.Ninjausers.FirstOrDefaultAsync(x => x.Id == id);

    if (dbItem == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(dbItem);
  }

  [HttpPost]
  public async Task<ActionResult<UserDTO>> AddUser(UserDTO aUser)
  {
    if (aUser == null)
    {
      return BadRequest("Empty User received");
    }

    var userdb = _mapper.Map<Ninjauser>(aUser);

    _context.Ninjausers.Add(userdb);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Log.Error(e, "Error Adding User");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
    
    return CreatedAtAction("AddUser", new { id = aUser.Id }, aUser);
  }
}
