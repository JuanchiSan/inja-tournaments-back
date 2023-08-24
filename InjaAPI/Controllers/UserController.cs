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
  [AllowAnonymous]
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
  [AllowAnonymous]
  public async Task<ActionResult<UserDTO>> GetPersonByEmail(string email)
  {

    var person = await _context.Injausers
      .FromSql($"SELECT * FROM injauser where lower(mail) = {email.ToLowerInvariant()}")
      .FirstOrDefaultAsync();
    // var person =
    //   await _context.Injausers.FirstOrDefaultAsync(x => x.Mail.ToLowerInvariant().Equals(email.ToLowerInvariant()));

    if (person == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(person);
  }

  [HttpGet("{id}")]
  [AllowAnonymous]
  public async Task<ActionResult<UserDTO>> GetUser(int id)
  {
    var dbItem = await _context.Injausers.FirstOrDefaultAsync(x => x.Id == id);

    if (dbItem == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(dbItem);
  }

  [HttpGet("GetContenderPoints")]
  [AllowAnonymous]
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
          ChallengeId = Convert.ToInt32(x.Challengeid),
          ChallengeName = x.Eventchallengename ?? string.Empty,
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
          PhotoURL = photoURL?.PhotoUrl ?? string.Empty,
          Hands = Convert.ToInt32(x.Hands),
          SlotStep = Convert.ToInt32(x.Slotstep)
        };

        for (var i = 1; i <= result.SlotCant; i++)
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

  [AllowAnonymous]
  [HttpGet("GetContenderParticipation")]
  [ProducesResponseType(StatusCodes.Status200OK),
  ProducesResponseType(StatusCodes.Status404NotFound),
  ProducesResponseType(StatusCodes.Status500InternalServerError)
  ]
  public async Task<ActionResult<UserParticipationDTO?>> GetContenderParticipation(int contenderId)
  {
    //var dbUser = await _context.Injausers.FirstOrDefaultAsync(x => x.Id == contenderId);
    var dbData = await _context.VUserpoints.Where(x => x.Contenderid == contenderId).ToListAsync();
  
    if (!dbData.Any())
    {
      return Ok(null);
    }
    
    try
    {
      var userPart = new UserParticipationDTO
      {
        ContenderId = contenderId,
        ContenderNumber = Convert.ToInt32(dbData.First().Contendernumber),
        Name = dbData.First().Contendername!
      };

      var dbPhotos = await _context.Photos.Where(x => x.Contenderid == contenderId).ToListAsync();
      
      userPart.Events?.AddRange(dbData
        .Select(x => new UserEventDTO
        {
          EventId = (int)x.Eventid!,
          EventName = x.Eventname!
        }).DistinctBy(y => y.EventId));

      userPart.Events?.ForEach(ue =>
      {
        ue.UserCompetitions?
          .AddRange(dbData
            .Where(y => y.Eventid == ue.EventId)
            .Select(z => new UserCompetitionsDTO
            {
              CompetitionId = (int)z.Competitiontypeid!,
              CompetitionName = z.Competitiontypename!
            })
            .DistinctBy(d => d.CompetitionId)
          );
        foreach (var userCompetitionsDto in ue.UserCompetitions!)
        {
          userCompetitionsDto.Challenges?.AddRange(dbData
            .Where(c => c.Eventid == ue.EventId && c.Competitiontypeid == userCompetitionsDto.CompetitionId)
            .Select(c => new UserEventChallengesDTO
            {
              ChallengeId = (int)c.Challengeid!,
              EventChallengeId = (int)c.Eventchallengeid!,
              EventChallengeName = c.Eventchallengename ?? string.Empty
            })
            .DistinctBy(d => d.EventChallengeId)
          );
          foreach (var challengesDto in userCompetitionsDto.Challenges!)
          {
            challengesDto.Division?.AddRange(dbData
              .Where(d => d.Eventid == ue.EventId && d.Competitiontypeid == userCompetitionsDto.CompetitionId && d.Eventchallengeid == challengesDto.EventChallengeId)
              .Select(c => new UserEventChallengeDivisionDTO
              {
                DivisionId = (int)c.Divisionid!,
                DivisionName = c.Divisionname!,
                MaxPoints = (decimal)c.Maxscore!,
                UserPoints = c.Totalpoints,
                PhotoUrl = dbPhotos.
                  Where(ph=>ph.Divisionid == c.Divisionid && ph.Contenderid == contenderId && ph.Eventid == ue.EventId && ph.Challengeid == challengesDto.ChallengeId).
                  MaxBy(ph1=>ph1.Created)?.PhotoUrl ?? string.Empty
              })
              .DistinctBy(d => d.DivisionId));

            foreach (var divisionDto in challengesDto.Division!)
            {
              #region Deducciones

              divisionDto.Deductions?.AddRange(
                _context
                  .VDeductions
                  .Where(dbd => dbd.Contenderid == contenderId && dbd.Eventid == ue.EventId && dbd.Challengeid == challengesDto.ChallengeId)
                  .Select(s => new UserPointsDeductionsDTO
                  {
                    DeductionId = (int)s.Deductionnumber!,
                    JudgeId = (int)s.Judgeid!,
                    JudgeName = s.Judgename!,
                    Comment = s.Comment ?? string.Empty,
                    Points = (decimal)s.Score!
                  }).ToList());

              #endregion

              #region Criterios

              divisionDto.Points?.AddRange(dbData
                .Where(d => d.Eventid == ue.EventId && d.Competitiontypeid == userCompetitionsDto.CompetitionId && d.Eventchallengeid == challengesDto.EventChallengeId &&
                            d.Divisionid == divisionDto.DivisionId)
                .Select(s => new UserEventChallengeCriteriasDTO
                {
                  CriteriaId = (int)s.Criteriaid!,
                  CantSlots = (int)s.Slotcant!,
                  CriteriaName = s.Criterianame ?? string.Empty,
                  JudgeId = s.Judgeid,
                  JudgeName = s.Judgename,
                  UserPoints = s.Totalpoints,
                  MaxPoints = (int)s.Maxscore!
                }).ToList());
              foreach (var dtoPoint in divisionDto.Points!)
              {
                var dbFinalPoint = dbData
                  .FirstOrDefault(d => d.Eventid == ue.EventId && d.Competitiontypeid == userCompetitionsDto.CompetitionId
                                                               && d.Eventchallengeid == challengesDto.EventChallengeId && d.Divisionid == divisionDto.DivisionId &&
                                                               d.Criteriaid == dtoPoint.CriteriaId);
                for (var i = 1; i <= dtoPoint.CantSlots; i++)
                {
                  var value = dbFinalPoint?.GetType().GetProperty($"Slot{i}")?.GetValue(dbFinalPoint);
                  dtoPoint.Points?.Add(new UserPointsDTO
                  {
                    SlotId = i,
                    Points = (decimal?)value
                  });
                }
              }

              #endregion
            }
          }
        }
      });
      return Ok(userPart);
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, "Error on GetContenderParticipation");
      return new ContentResult
      {
        StatusCode = StatusCodes.Status500InternalServerError,
        Content = "Error: " + e.Message
      };
    }
  }

  [AllowAnonymous]
  [HttpPost("NewContender")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO)),
  ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string)),
  ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
  public async Task<ActionResult<int>> NewUser(UserDTO aNewUser)
  {
    var dbUser = await _context.Injausers.FirstOrDefaultAsync(x => x.Mail == aNewUser.Mail);
    if (dbUser != null)
    {
      return BadRequest("User already exists");
    }

    dbUser = await _context.Injausers.FirstOrDefaultAsync(x => x.Docnumber == aNewUser.Docnumber && x.Docid == aNewUser.Docid);
    if (dbUser != null)
    {
      return BadRequest("User Document already exists");
    }

    var newDbUser = new Injauser
    {
      Active = true,
      Pass = aNewUser.Pass,
      Firstname = aNewUser.Firstname,
      Lastname = aNewUser.Lastname,
      Adminusertype = 1,
      Cityid = aNewUser.Cityid,
      Docid = aNewUser.Docid,
      Docnumber = aNewUser.Docnumber,
      Mail = aNewUser.Mail,
      Phone = aNewUser.Phone,
      Street = aNewUser.Street,
    };

    _context.Injausers.Add(newDbUser);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, "Error saving new user");
      return  StatusCode(StatusCodes.Status500InternalServerError, "Error saving new user");
    }
    
    return Ok(newDbUser.Id);  
  }
}