using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using InjaDTO;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;

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

  [AllowAnonymous]
  [HttpGet("GetContenderPoints")]
  public async Task<ActionResult<List<ContenderPointsResponseDTO>>> GetContenderPoints(int eventId, int challengeid, int divisionId, int contenderId)
  {
    try
    {
      var lst = await _context.VUserpoints
        .Where(x => x.Eventid == eventId && x.Challengeid == challengeid && x.Divisionid == divisionId && x.Contenderid == contenderId)
        .ToListAsync();

      if (!lst.Any())
        return NotFound("No Data with the given parameters");

      var lstResult = new List<ContenderPointsResponseDTO>();
      var photoURL = await _context
        .Photos
        .Where(x => x.Eventid == eventId && x.Challengeid == challengeid && x.Contenderid == contenderId && x.Divisionid == divisionId)
        .OrderByDescending(y => y.Created)
        .FirstOrDefaultAsync();
      foreach (var x in lst)
      {
        var result = new ContenderPointsResponseDTO
        {
          ContenderId = contenderId,
          ContenderName = x.Contendername == null ? string.Empty : x.Contendername!,
          JudgeId = (int)x.Judgeid!,
          JudgeName = x.Judgename ?? string.Empty,
          CriteriaId = Convert.ToInt32(x.Criteriaid),
          CriteriaName = x.Criterianame ?? string.Empty,
          EventJugdeChallengeDivisionId = Convert.ToInt32(x.Eventjudgechallengedivisionid),
          Round = Convert.ToInt32(x.Rounds),
          TotalPoints = Convert.ToDecimal(x.Totalpoints),
          MaxPoints = Convert.ToDecimal(x.Maxscore),
          Comment = x.Comment ?? string.Empty,
          SlotCant = Convert.ToInt32(x.Slotcant),
          DivisionId = Convert.ToInt32(x.Divisionid),
          DivisionName = x.Divisionname ?? string.Empty,
          CompetitionId = Convert.ToInt32(x.Competitiontypeid),
          CompetitinoName = x.Competitiontypename ?? string.Empty,
          PhotoURL = photoURL?.PhotoUrl ?? string.Empty
        };

        for (int i = 1; i <= result.SlotCant; i++)
        {
          result.Points.Add(Convert.ToDecimal(x.GetType().GetProperty($"Slot{i}")?.GetValue(x)));
        }
        
        lstResult.Add(result);
      }

      return Ok(lstResult);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error ar GetContenderPoints");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  // [AllowAnonymous]
  // [HttpPost("RecoverUser")]
  // public async Task<ActionResult> RecoverUser(string anEmail)
  // {
  //   return Ok();
  // }

  [HttpPost("AddUser")]
  public async Task<ActionResult<UserDTO>> AddUser(UserDTO? aUser)
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