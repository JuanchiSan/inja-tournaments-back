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
    var dbItems = await _context.Events.ToListAsync();
    var result = new List<EventDTO>();
    if (!dbItems.Any()) return result;

    result.AddRange(dbItems.Select(dbItem => new EventDTO
    {
      Id = dbItem.Id,
      StartDate = dbItem.Startdate,
      EndDate = dbItem.Enddate,
      InscriptionStartDate = dbItem.Startinscription,
      InscriptionEndDate = dbItem.Endinscription,
      Name = dbItem.Name
    }));
    return result;
  }

  [AllowAnonymous]
  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EventDTO)),
   ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<EventDTO>> GetEvent(int id)
  {
    EventDTO? result = null;
    var dbItems = await _context.VEventchallengedivisionPlanas.Where(x => x.Eventid == id).ToListAsync();

    if (!dbItems.Any())
    {
      return NotFound();
    }

    foreach (var dbItem in dbItems)
    {
      if (result == null)
        result = new EventDTO
        {
          Id = id,
          Name = dbItem.Eventname!,
          StartDate = dbItem.Eventstartdate,
          EndDate = dbItem.Eventenddate,
          InscriptionStartDate = dbItem.Eventstartincriptiondate,
          InscriptionEndDate = dbItem.Eventendincriptiondate
        };
      var memCompType = result.CompetitionTypes.FirstOrDefault(x => x.Id == dbItem.Competitiontypeid);
      if (memCompType == null)
      {
        memCompType = new CompetitionTypeDTO
        {
          Id = (int)dbItem.Competitiontypeid!,
          Name = dbItem.Competitiontypename!,
        };
        result.CompetitionTypes.Add(memCompType);
      }
      var memChallenge = memCompType.Challenges.FirstOrDefault(x => x.EventChallengeId == dbItem.Eventchallengeid);
      if (memChallenge == null)
      {
        memChallenge = new ChallengeDTO
        {
          Id = (int)dbItem.Eventchallengeid!,
          Name = dbItem.Eventchallengename!,
          ChallengeStartDate = dbItem.Eventchallengestartdate,
          ChallengeEndDate = dbItem.Eventchallengeenddate,
          ChallengeId = (int)dbItem.Challengeid!,
          ChallengeName = dbItem.Challengename!,
          EventChallengeId = (int)dbItem.Eventchallengeid!,
          EventChallengeName = dbItem.Eventchallengename!,
        };
        memCompType.Challenges.Add(memChallenge);
      }
      
      var memDivision = memChallenge.Divisions.FirstOrDefault(x => x.Id == dbItem.Divisionid);
      if (memDivision == null)
      {
        memDivision = new DivisionDTO
        {
          Id = (int)dbItem.Divisionid!,
          Name = dbItem.Divisionname!,
        };
        memChallenge.Divisions.Add(memDivision);
      }
    }

    return Ok(result);
  }
}