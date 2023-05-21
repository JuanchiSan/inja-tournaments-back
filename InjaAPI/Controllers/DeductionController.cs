using AutoMapper;
using InjaData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class DeductionController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;

  public DeductionController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  [AllowAnonymous]
  [HttpPut("AddDeduction")]
  public async Task<ActionResult<int>> AddDeduction(int deductionnumber, int eventId, int challengeid, int divisionid, int judgeid, int contenderid, decimal score, string? comment)
  {
    var dbContender = await _context
      .Injausers.Include(x => x.Injauserusertypes)
      .FirstOrDefaultAsync(y => y.Id == contenderid && y.Injauserusertypes.Any(z => z.Eventid == eventId && z.Typeid == 1));

    if (dbContender == null)
    {
      return BadRequest("Contender does not exists or does not belong to the given Event");
    }
    
    var dbJudge = await _context
      .Injausers.Include(x => x.Injauserusertypes)
      .FirstOrDefaultAsync(y => y.Id == judgeid &&
                                y.Injauserusertypes.Any(z => z.Eventid == eventId && z.Userid == judgeid && z.Typeid == 6));

    if (dbJudge == null)
    {
      return BadRequest("Judge not found or isn't a Supervisor");
    }

    var dbInscription = await _context
      .Userinscriptions
      .Include(ec => ec.Eventchallengedivision)
      .ThenInclude(y => y.Eventchallenge)
      .FirstOrDefaultAsync(x => x.Ueventid == eventId && x.Utypeid == 1 && x.Uuserid == contenderid && x.Divisionid == divisionid &&
                                x.Eventchallengedivision.Eventchallenge.Challengeid == challengeid);

    if (dbInscription == null)
    {
      return BadRequest("The Contender isn't enrolled in the given challenge/division");
    }

    var maxDeductionNumberss = await _context
      .Deductions
      .Where(x => x.Suptypeid == 6 && x.Supeventid == eventId && x.Ceventid == eventId && x.Cuserid == contenderid && x.Ctypeid == 1 && x.Cdivisionid == divisionid &&
                  x.Ceventchallengeid == dbInscription.Eventchallengeid)
      .ToListAsync();
    
    int maxDeductionNumber = 0;
    
    if (maxDeductionNumberss.Any())          
      maxDeductionNumber = maxDeductionNumberss.Max(x => x.Deductionid);
    
    var ndeductionNumber = deductionnumber == -1 ? maxDeductionNumber + 1 : maxDeductionNumber;
  
    var dbDeduction = await _context
      .Deductions
      .FirstOrDefaultAsync(x => x.Deductionid == ndeductionNumber && x.Suptypeid == 6 && x.Supeventid == eventId && x.Ceventid == eventId && x.Cuserid == contenderid && x.Ctypeid == 1 && x.Cdivisionid == divisionid && x.Ceventchallengeid == dbInscription.Eventchallengeid);

    if (dbDeduction == null) // No está, hay que agregarlo
    {
      dbDeduction = new Deduction
      {
        Deductionid = ndeductionNumber,
        Suptypeid = 6,
        Supeventid = eventId,
        Supuserid = judgeid,
        Ceventid = eventId,
        Cuserid = contenderid,
        Ctypeid = 1,
        Cdivisionid = divisionid,
        Ceventchallengeid = dbInscription.Eventchallengeid,
        Updatedate = null,
        Creationdate = DateTime.Now,
        Comment = comment,
        Score = score
      };
      _context.Deductions.Add(dbDeduction);
    }
    else
    {
      dbDeduction.Score = score;
      dbDeduction.Updatedate = DateTime.Now;
    }

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error at Deduction");
      return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
    }
    
    return Ok(ndeductionNumber);
  }

  [AllowAnonymous]
  [HttpDelete("DeleteDeduction")]
  public async Task<ActionResult> DeleteDeduction(int deductionnumber, int eventId, int challengeid, int divisionid, int judgeid, int contenderid)
  {
    var dbDeduction = await _context
      .Deductions
      .Include(x => x.C)
      .ThenInclude(y => y.Eventchallengedivision)
      .ThenInclude(z => z.Eventchallenge)
      .FirstOrDefaultAsync(x => x.Deductionid == deductionnumber
                                && x.Supeventid == eventId && x.Suptypeid == 6 && x.Supuserid == judgeid
                                && x.Ceventid == eventId && x.Ctypeid == 1 && x.Cuserid == contenderid
                                && x.Cdivisionid == divisionid
                                && x.C.Eventchallengedivision.Eventchallenge.Challengeid == challengeid);

    if (dbDeduction == null)
    {
      return NotFound();
    }
    _context.Deductions.Remove(dbDeduction);

    try
    {
      await _context.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error at Delete Deduction");
      return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {e}");
    }
  }
  
  [AllowAnonymous]
  [HttpGet("ContenderDeductions")]
  public async Task<ActionResult<List<VDeduction>>> ContenderDeductions(int eventId, int contenderId)
  {
    try
    {
      var dbDeductions = await _context
        .VDeductions
        .Where(x => x.Eventid == eventId && x.Contenderid == contenderId)
        .ToListAsync();

      if (!dbDeductions.Any())
        return Ok(new List<VDeduction>());
      
      return dbDeductions;
      
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error on ContenderDeduction");
      return StatusCode(StatusCodes.Status500InternalServerError, "Se rompió");
    }
  }
}