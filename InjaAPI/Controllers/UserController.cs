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
    Serilog.Log.Logger.Information("{CurrentMethod} from IP: {RemoteIpAddress}",
      System.Reflection.MethodBase.GetCurrentMethod()?.Name,
      Helper.GetRemoteIpAddress(HttpContext));
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
    Serilog.Log.Logger.Information("{CurrentMethod} email: {EMail} from IP: {RemoteIpAddress}",
      System.Reflection.MethodBase.GetCurrentMethod()?.Name,
      email,
      Helper.GetRemoteIpAddress(HttpContext));

    var person = await _context.Injausers
      .FromSql($"SELECT * FROM injauser where lower(mail) = {email.ToLowerInvariant()}")
      .FirstOrDefaultAsync();

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
    Serilog.Log.Logger.Information("{CurrentMethod} Id: {EMail} from IP: {RemoteIpAddress}",
      System.Reflection.MethodBase.GetCurrentMethod()?.Name,
      id,
      Helper.GetRemoteIpAddress(HttpContext));

    var dbItem = await _context.Injausers.FirstOrDefaultAsync(x => x.Id == id);

    if (dbItem == null)
    {
      return NotFound();
    }

    return _mapper.Map<UserDTO>(dbItem);
  }

  [HttpGet("GetContenderPoints")]
  [AllowAnonymous]
  public async Task<ActionResult<List<ContenderPointsResponseDTO>>> GetContenderPoints(int eventId, int challengeid,
    int divisionId, int contenderId)
  {
    Serilog.Log.Logger.Information(
      "{CurrentMethod} for ContenderId: {ContenderId} eventId: {EventId} challengeId: {ChallengeId}, DivisionId: {DivisionId} from IP: {RemoteIpAddress}",
      System.Reflection.MethodBase.GetCurrentMethod()?.Name,
      contenderId, eventId, challengeid, divisionId,
      Helper.GetRemoteIpAddress(HttpContext));
    
    try
    {
      var dicCriteriaNames = await _context.Judgmentcriteria.ToDictionaryAsync(x=>x.Id);
      var lst = await _context.VUserpoints
        .Where(x => x.Eventid == eventId && x.Challengeid == challengeid && x.Divisionid == divisionId &&
                    x.Contenderid == contenderId)
        .ToListAsync();

      if (!lst.Any())
      {
        Serilog.Log.Logger.Information("No Data Found");
        return NotFound("No Data with the given parameters");
      }

      var lstResult = new List<ContenderPointsResponseDTO>();
      var photoURL = await _context
        .Photos
        .Where(x => x.Eventid == eventId && x.Challengeid == challengeid && x.Contenderid == contenderId &&
                    x.Divisionid == divisionId)
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
          CriteriaNames = Helper.GetCriteriaNames(Convert.ToInt32(x.Criteriaid), dicCriteriaNames),
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
          SlotStep = Convert.ToDecimal(x.Slotstep)
        };

        for (var i = 1; i <= result.SlotCant; i++)
        {
          result.Points.Add(Convert.ToDecimal(x.GetType().GetProperty($"Slot{i}")?.GetValue(x)));
        }

        lstResult.Add(result);
      }

      Serilog.Log.Logger.Information("Returning Data {DataCount}", lstResult.Count);
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
   ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult<UserParticipationDTO?>> GetContenderParticipation(int contenderId)
  {
    Serilog.Log.Logger.Information("{CurrentMethod} for contenderId: {ContenderId} from IP: {RemoteIpAddress}",
      System.Reflection.MethodBase.GetCurrentMethod()?.Name,
      contenderId,
      Helper.GetRemoteIpAddress(HttpContext));

    var dbDicCriteriaNames = await _context.Judgmentcriteria.ToDictionaryAsync(x => x.Id);
    var dbData = await _context.VUserpoints.Where(x => x.Contenderid == contenderId).OrderByDescending(x=>x.Eventid).ThenBy(y=>y.Rounds).ToListAsync();
    var dbDeductions = await _context.VDeductions.Where(x => x.Contenderid == contenderId).ToListAsync();
    var dbDataWinCD = await _context.VWinnersByChallengeDivisions.Where(x => x.Contenderid == contenderId).ToListAsync();
    var dbPhotos = await _context.Photos.Where(x => x.Contenderid == contenderId).ToListAsync();
    
    #region Cups
    var dbDataNailArt = await _context.VCupNailArts.Where(x => x.Contenderid == contenderId).ToListAsync();
    var dbWinofWin = await _context.VWinnerOfWinners.Where(x => x.Contenderid == contenderId).ToListAsync();
    var dbGrandChampion = await _context.VGrandChampions.Where(x => x.Contenderid == contenderId).ToListAsync();
    var dbDataGroupCup = await _context.VInjagroupPointsPlanas.Where(x => x.Contenderid == contenderId).ToListAsync();
    #endregion
    
    if (!dbData.Any())
    {
      Serilog.Log.Logger.Information("No Data Found");

      return Ok(null);
    }

    try
    {
      var userPart = new UserParticipationDTO
      {
        ContenderId = contenderId,
        ContenderNumber = Convert.ToInt32(dbData.First().Contendernumber),
        Name = dbData.First().Contendername!,
        Events = new List<UserEventDTO>()
      };

      foreach (var dbItem in dbData)
      {
        // nuevo evento
        var eventItem = userPart.Events.FirstOrDefault(x => x.EventId == dbItem.Eventid);
        if (eventItem == null)
        {
          eventItem = new UserEventDTO
          {
            EventId = (int)dbItem.Eventid!,
            EventName = dbItem.Eventname!,
            PointsPublished = Convert.ToBoolean(dbItem.PointPublished),
            PointsPublishedDate = dbItem.PointPublishedDate,
            PointsPublishedUserAuth = dbItem.PointPublishedUser,
            NotPublishedPointsMessage = dbItem.PointPublishedMessage
          };
          userPart.Events.Add(eventItem);
        }
        // Competition Type
        var compTypeItem = eventItem.UserCompetitions?.FirstOrDefault(x => x.CompetitionId == dbItem.Competitiontypeid);
        if (compTypeItem == null) // Nuevo tipo de competencia
        {
          compTypeItem = new UserCompetitionsDTO
          {
            CompetitionId = (int)dbItem.Competitiontypeid!,
            CompetitionName = dbItem.Competitiontypename!
          };
          eventItem.UserCompetitions?.Add(compTypeItem);
        }
        //Challenge
        var challengeItem = compTypeItem.Challenges?.FirstOrDefault(x => x.EventChallengeId == dbItem.Eventchallengeid);
        if (challengeItem == null) // Nuevo Challenge
        {
          challengeItem = new UserEventChallengesDTO
          {
            ChallengeId = (int)dbItem.Challengeid!,
            EventChallengeId = (int)dbItem.Eventchallengeid!,
            EventChallengeName = dbItem.Eventchallengename ?? string.Empty,
            MaxRoundCant =
              Convert.ToInt32(dbData.Where(x => x.Eventchallengeid == dbItem.Eventchallengeid).Max(y => y.Rounds))
          };
          compTypeItem.Challenges?.Add(challengeItem);
        }
        // Division
        var divisionItem = challengeItem.Division?.FirstOrDefault(x => x.DivisionId == dbItem.Divisionid);
        if (divisionItem == null) // Nueva División
        {
          divisionItem = new UserEventChallengeDivisionDTO
          {
            DivisionId = (int)dbItem.Divisionid!,
            DivisionName = dbItem.Divisionname!,
            MaxPoints = Convert.ToDecimal(dbData.Where(x=>x.Eventid == dbItem.Eventid && x.Divisionid == dbItem.Divisionid && x.Eventchallengeid == dbItem.Eventchallengeid).Sum(y=>y.Maxscore)),
            UserPoints = Convert.ToDecimal(dbData.Where(x=>x.Eventid == dbItem.Eventid && x.Divisionid == dbItem.Divisionid && x.Eventchallengeid == dbItem.Eventchallengeid).Sum(y=>y.Totalpoints)),
            UserPosition = Convert.ToInt32(
              dbDataWinCD.FirstOrDefault(ucd =>
                ucd.Contenderid == contenderId && ucd.Divisionid == (int)dbItem.Divisionid &&
                ucd.Eventid == dbItem.Eventid && ucd.Eventchallengeid == dbItem.Eventchallengeid)?.Rank),
            PhotoUrl = dbPhotos
              .Where(ph =>
                ph.Divisionid == dbItem.Divisionid && ph.Contenderid == contenderId && ph.Eventid == dbItem.Eventid &&
                ph.Challengeid == dbItem.Challengeid).MaxBy(ph1 => ph1.Created)?.PhotoUrl ?? string.Empty
          };
          challengeItem.Division?.Add(divisionItem);
          
          #region Deducciones

          divisionItem.Deductions?.AddRange(dbDeductions
            .Where(dbd => dbd.Contenderid == contenderId && dbd.Eventid == dbItem.Eventid &&
                          dbd.Challengeid == dbItem.Challengeid)
            .Select(s => new UserPointsDeductionsDTO
            {
              DeductionId = (int)s.Deductionnumber!,
              JudgeId = (int)s.Judgeid!,
              JudgeName = s.Judgename!,
              Comment = s.Comment ?? string.Empty,
              Points = (decimal)s.Score!
            })
            .ToList());

          #endregion
        }

        var ueCriteriaItem = divisionItem.Points.FirstOrDefault(x => x.CriteriaId == dbItem.Criteriaid && x.Round == dbItem.Rounds);
        if (ueCriteriaItem == null) // Nuevo criterio
        {
          ueCriteriaItem = new UserEventChallengeCriteriasDTO
          {
            CriteriaId = (int)dbItem.Criteriaid!,
            CantSlots = (int)dbItem.Slotcant!,
            CriteriaName = dbItem.Criterianame ?? string.Empty,
            CriteriaNames = Helper.GetCriteriaNames(Convert.ToInt32(dbItem.Criteriaid), dbDicCriteriaNames),
            Round = dbItem.Rounds,
            JudgeId = dbItem.Judgeid,
            JudgeName = dbItem.Judgename,
            UserPoints = dbItem.Totalpoints,
            MaxPoints = (int)dbItem.Maxscore!
          };
          for (var i = 1; i <= dbItem.Slotcant; i++)
          {
            var value = dbItem.GetType().GetProperty($"Slot{i}")?.GetValue(dbItem);
            ueCriteriaItem.Points?.Add(new UserPointsDTO
            {
              SlotId = i,
              Points = (decimal?)value
            });
          }
          
          divisionItem.Points.Add(ueCriteriaItem);
        }
      }

      userPart.Events?.ForEach(ue =>
      {
        #region Copas

        var dbCupArtEvent = dbDataNailArt.Where(x => x.Eventid == ue.EventId).ToList();
        foreach (var dbItem in dbCupArtEvent)
        {
          ue.UserCups.Add(new UserCupsDTO
          {
            CupName = "Nail Art",
            DivisionName = dbItem.Divisionname!,
            TotalPoints = Convert.ToDecimal(dbItem.FinalPoint),
            Position = Convert.ToInt32(dbItem.Rank)
          });
        }

        var dbCupWinofWinCup = dbWinofWin.Where(x => x.Eventid == ue.EventId).ToList();
        foreach (var dbItem in dbCupWinofWinCup)
        {
          ue.UserCups.Add(new UserCupsDTO
          {
            CupName = "Winner of Winners",
            DivisionName = dbItem.Divisionname!,
            TotalPoints = Convert.ToDecimal(dbItem.Finalpoints),
            Position = Convert.ToInt32(dbItem.Rank)
          });
        }
        
        var dbGrandChampionCup = dbGrandChampion.Where(x => x.Eventid == ue.EventId).ToList();
        foreach (var dbItem in dbGrandChampionCup)
        {
          ue.UserCups.Add(new UserCupsDTO
          {
            CupName = "Grand Champion",
            TotalPoints = Convert.ToDecimal(dbItem.Finalpoints),
            Position = Convert.ToInt32(dbItem.Rank)
          });
        }
        
        #endregion

        #region Los Grupos

        foreach (var dbItem in dbDataGroupCup)
        {
          ue.UserGroups.Add(new UserPointGroupDTO
          {
            EventChallengeName = dbItem.Eventchallengename ?? string.Empty,
            GroupName = dbItem.Groupname!,
            GroupPosition = Convert.ToInt32(dbItem.GroupPosition),
            TotalPoints = Convert.ToDecimal(dbItem.GroupPoints)
          });
        }

        #endregion
      });

      Serilog.Log.Logger.Information("Returning Data");

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
  [HttpPut("ChangeUserLanguage")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string)),
   ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string)),
   ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
  public async Task<ActionResult> ChangeUserLanguage(string anEmail, string newLanguage)
  {
    try
    {
      Serilog.Log.Logger.Information("{CurrentMethod} for User: {Contender} from IP: {RemoteIpAddress}",
        System.Reflection.MethodBase.GetCurrentMethod()?.Name,
        (anEmail + ", newLanguage" + newLanguage),
        Helper.GetRemoteIpAddress(HttpContext));
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, $"Error on {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
    }

    if (Helper.Languages.FirstOrDefault(x =>
          string.Compare(x, newLanguage, StringComparison.InvariantCultureIgnoreCase) == 0) == null)
    {
      return BadRequest($"Bad Language, the possible values are:{Helper.Languages}");
    }

    var dbUser = (await _context.Injausers.ToListAsync())
      .FirstOrDefault(x => string.Compare(x.Mail, anEmail, StringComparison.InvariantCultureIgnoreCase) == 0);
    if (dbUser == null)
    {
      return BadRequest("User Mail does not exists");
    }

    try
    {
      dbUser.PreferredLanguage = newLanguage;
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, $"Error on {System.Reflection.MethodBase.GetCurrentMethod()?.Name}");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error Changing the language");
    }
    
    return Ok("The Change was realized");
  }
  
  [AllowAnonymous]
  [HttpPost("NewContender")]
  [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO)),
   ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string)),
   ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
  public async Task<ActionResult<int>> NewUser(UserDTO aNewUser)
  {
    try
    {
      Serilog.Log.Logger.Information("{CurrentMethod} for User: {Contender} from IP: {RemoteIpAddress}",
        System.Reflection.MethodBase.GetCurrentMethod()?.Name,
        (aNewUser.Lastname + ", " + aNewUser.Firstname),
        Helper.GetRemoteIpAddress(HttpContext));
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, "Error on NewContender");
    }

    var dbUser = await _context.Injausers.FirstOrDefaultAsync(x => x.Mail == aNewUser.Mail);
    if (dbUser != null)
    {
      return BadRequest("User Mail already exists");
    }

    dbUser = await _context.Injausers.FirstOrDefaultAsync(x =>
      x.Docnumber == aNewUser.Docnumber && x.Docid == aNewUser.Docid);
    if (dbUser != null)
    {
      return BadRequest("User Document already exists");
    }

    if (aNewUser.CountryId is null or 0)
    {
      var rCountry = await Helper.AddCountry(_context, aNewUser.CountryName ?? "Desconocido");
      if (rCountry.Item1 != 200)
        return StatusCode(rCountry.Item1, "Error adding a new Country in NewUser");
      var aCountry = rCountry.Item2;
      aNewUser.CountryId = aCountry?.Id;
    }

    if (aNewUser.Cityid is null or 0)
    {
      var raCity = await Helper.AddCity(_context, aNewUser.CityName!, aNewUser.CountryName);
      if (raCity.Item1 != 200)
        return StatusCode(raCity.Item1, "Error adding a new City in NewUser");
      var aCity = raCity.Item2;
      aNewUser.Cityid = aCity?.Id;
    }

    var newDbUser = new Injauser
    {
      Active = true,
      Pass = aNewUser.Pass,
      Firstname = aNewUser.Firstname,
      Lastname = aNewUser.Lastname,
      Adminusertype = 0,
      Cityid = aNewUser.Cityid,
      Docid = aNewUser.Docid,
      Docnumber = aNewUser.Docnumber,
      Mail = aNewUser.Mail,
      Phone = aNewUser.Phone,
      Street = aNewUser.Street,
      PreferredLanguage = aNewUser.Language
    };

    _context.Injausers.Add(newDbUser);
    try
    {
      Serilog.Log.Logger.Information("Saving new user");
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Logger.Error(e, "Error saving new user");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error saving new user");
    }

    Serilog.Log.Logger.Information("Returning new user id");
    return Ok(newDbUser.Id);
  }
}