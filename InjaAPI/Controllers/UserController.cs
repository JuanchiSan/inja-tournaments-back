using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using Serilog;
using AutoMapper;
using InjaDTO;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using NuGet.Common;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class UserController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;

  public UserController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  [HttpGet("GetUsers")]
  public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
  {
    try
    {
      return await _context.Injausers.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  // GET: api/UserByEmail/jperez@pepe
  [HttpGet("UserByEmail/{email}")]
  public async Task<ActionResult<UserDTO>> GetPersonByEmail(string email)
  {
    var person =
      await _context.Injausers.FirstOrDefaultAsync(x => x.Mail.ToLowerInvariant().Equals(email.ToLowerInvariant()));

    if (person == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(person);
  }

   [HttpGet("{id}")]
  public async Task<ActionResult<UserDTO>> GetUser(int id)
  {
    var dbItem = await _context.Injausers.FirstOrDefaultAsync(x => x.Id == id);

    if (dbItem == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(dbItem);
  }

  // [AllowAnonymous]
  // [HttpPost("RecoverUser")]
  // public async Task<ActionResult> RecoverUser(string anEmail)
  // {
  //   return Ok();
  // }

  [HttpPost("AddUser")]
  public async Task<ActionResult<UserDTO>> AddUser(UserDTO aUser)
  {
    if (aUser == null)
    {
      return BadRequest("Empty User received");
    }

    var userdb = _mapper.Map<Injauser>(aUser);

    _context.Injausers.Add(userdb);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error Adding User");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
    
    return CreatedAtAction("AddUser", new { id = aUser.Id }, aUser);
  }
}
