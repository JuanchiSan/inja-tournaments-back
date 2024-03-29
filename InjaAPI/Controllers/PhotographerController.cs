﻿using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using InjaData.Models;
using AutoMapper;
using InjaDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace InjaAPI.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class PhotographerController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;

  public PhotographerController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  [HttpGet("GetImage")]
  [AllowAnonymous]
  public async Task<ActionResult> GetImage(int eventId, int challengeId, int divisionId, int contenderId, int photographerId)
  {
    Serilog.Log.Logger.Information("{CurrentMethod} event: {EventId} challengeId:{ChallengeId} divisionId: {DivId} Contender: {ContenderId}  PhotographerId:{PhotoId} from IP: {RemoteIpAddress}", 
      System.Reflection.MethodBase.GetCurrentMethod()?.Name, 
      eventId, challengeId, divisionId, contenderId, photographerId,
      Helper.GetRemoteIpAddress(HttpContext));
    
    try
    {
      var cosa = await new dbContext().Photos
        .Where(x => x.Eventid == eventId && x.Challengeid == challengeId && x.Divisionid == divisionId && x.Contenderid == contenderId && 
                    (photographerId == -1 || x.Photographerid == photographerId))
        .OrderByDescending(x => x.Created)
        .ToListAsync();

      if (!cosa.Any())
        return NotFound();

      var imagePath = Path.Combine("./Photos", cosa.First().StoredFileName);

      if (!System.IO.File.Exists(imagePath))
      {
        return NotFound();
      }

      var imageBytes = await System.IO.File.ReadAllBytesAsync(imagePath);

      return File(imageBytes, "image/png");
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error Reading Photo");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
    }
  }

  private string GetFileName(int eventId, int challengeId, int divisionid, int contenderId, int photographerId) => $"{eventId}_{challengeId}_{divisionid}_{contenderId}_{photographerId}";

  private string GetPhotoURL(int eventId, int challengeId, int divisionId, int contenderId, int photographerId) =>
    $"{Helper.APIDomain}/api/Photographer/DownloadPhotoFile?eventId={eventId}&challengeId={challengeId}&divisionid={divisionId}&contenderId={contenderId}&photographerId={photographerId}";


  [HttpGet("GetContenderChallenges")]
  [AllowAnonymous]
  public async Task<ActionResult<ResponseDivisionContenderChallengeDTO>> GetContenderChallenges(int eventid, int challengeid, int divisionid, int contenderid)
  {
    Serilog.Log.Logger.Information("{CurrentMethod} event: {EventId} challengeId:{ChallengeId} divisionId: {DivId} Contender: {ContenderId} from IP: {RemoteIpAddress}", 
      System.Reflection.MethodBase.GetCurrentMethod()?.Name, 
      eventid, challengeid, divisionid, contenderid,
      Helper.GetRemoteIpAddress(HttpContext));
    
    var dbItems = await _context
      .VUserinscriptionPlanas
      .Where(x => x.Eventid == eventid && x.Challengeid == challengeid)
      .ToListAsync();

    if (dbItems.All(x => x.Userid != contenderid)) return NotFound();

    var dbPhoto = _context
      .Photos
      .Where(x => x.Challengeid == challengeid && x.Eventid == eventid && x.Contenderid == contenderid)
      .OrderByDescending(x => x.Created).FirstOrDefault();

    var contenderName = _context.Injausers.FirstOrDefault(x => x.Id == contenderid)?.Name;
    if (contenderName == null) return NotFound();
    var result = new ResponseDivisionContenderChallengeDTO
    {
      EventId = eventid,
      EventName = _context.Events.FirstOrDefault(x => x.Id == eventid)!.Name,
      ChallengeId = challengeid,
      ChallengeName = _context.Eventchallenges.FirstOrDefault(x => x.Challengeid == challengeid && x.Eventid == eventid)!.Name,
      ContenderId = contenderid,
      ContenderName = contenderName,
      DivisionId = divisionid,
      DivisionName = _context.Divisions.FirstOrDefault(x => x.Id == divisionid)!.Name,
      PhotoURL = (dbPhoto == null ? string.Empty : dbPhoto.PhotoUrl) ?? string.Empty,
      TotalContendersThisChallenge = dbItems.Count,
      TotalPhotosInThisChallenge = _context.Photos.Count(x => x.Eventid == eventid && x.Challengeid == challengeid)
    };
    return Ok(result);
  }

  [HttpPost("UploadPhotoFile")]
  [AllowAnonymous]
  public async Task<IActionResult> UploadPhotoFile(int eventId, int challengeId, int divisionId, int contenderId, int photographerId, IFormFile? file)
  {
    Serilog.Log.Logger.Information("{CurrentMethod} event: {EventId} challengeId:{ChallengeId} divisionId: {DivId} Contender: {ContenderId}  PhotographerId:{PhotoId} from IP: {RemoteIpAddress}", 
      System.Reflection.MethodBase.GetCurrentMethod()?.Name, 
      eventId, challengeId, divisionId, contenderId, photographerId,
      Helper.GetRemoteIpAddress(HttpContext));
    try
    {
      if (file == null)
        return BadRequest("No Photo Received");

      var ct = DateTime.Now;

      if (file.Length <= 0) return BadRequest("File length is 0");
      var fileNameStorage = GetFileName(eventId, challengeId, divisionId, contenderId, photographerId) + $"_{ct.Year}_{ct.Month}_{ct.Day}_{ct.Hour}_{ct.Minute}_{ct.Second}";
      var photourl = GetPhotoURL(eventId, challengeId, divisionId, contenderId, photographerId);

      var dbPhoto = new Photo
      {
        Challengeid = challengeId,
        Contenderid = contenderId,
        Divisionid = divisionId,
        Photographerid = photographerId,
        Eventid = eventId,
        Created = ct,
        StoredFileName = fileNameStorage,
        Filename = file.FileName,
        PhotoUrl = photourl
      };

      _context.Photos.Add(dbPhoto);

      var filePath = Path.Combine("./Photos", fileNameStorage);

      await using var stream = System.IO.File.Create(filePath);
      await file.CopyToAsync(stream);
      await _context.SaveChangesAsync();
      
      Serilog.Log.Logger.Information("Returning Data on UploadPhotoFile");
      
      return Ok(photourl);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error Saving File");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  [HttpGet("DownloadPhotoFile")]
  [AllowAnonymous]
  public IActionResult DownloadPhotoFile(int eventId, int challengeId, int divisionId, int contenderId, int photographerId)
  {
    Serilog.Log.Logger.Information("{CurrentMethod} event: {EventId} challengeId:{ChallengeId} divisionId: {DivId} Contender: {ContenderId}  PhotographerId:{PhotoId} from IP: {RemoteIpAddress}", 
      System.Reflection.MethodBase.GetCurrentMethod()?.Name, 
      eventId, challengeId, divisionId, contenderId, photographerId,
      Helper.GetRemoteIpAddress(HttpContext));
    try
    {
      var dbItems = _context.Photos
        .Where(x => x.Divisionid == divisionId && x.Challengeid == challengeId && x.Eventid == eventId && x.Photographerid == photographerId && x.Contenderid == contenderId)
        .OrderByDescending(x => x.Created);
      if (!dbItems.Any())
        return NotFound("Image Not Found");

      var dbItem = dbItems.First();

      var filePath = Path.Combine("./Photos", dbItem.StoredFileName);

      Serilog.Log.Logger.Information("Returning File on DownloadPhotoFile");
      return File(System.IO.File.OpenRead(filePath), "image/png", string.IsNullOrEmpty(dbItem.Filename) ? "Image.png" : dbItem.Filename);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error Reading File");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  [HttpPost("UploadPhotoImageBase64")]
  [AllowAnonymous]
  public async Task<ActionResult<string>> UploadPhotoImageBase64(ImageUploadDTO obj)
  {
    if (obj.Photo64string == null)
    {
      return BadRequest("No Image or bad DTO");
    }

    try
    {
      #region Controls

      if (await _context.Injausers.FindAsync(obj.ContenderId) == null)
      {
        return BadRequest("Contender Not Found");
      }

      if (await _context.Events.FindAsync(obj.EventId) == null)
      {
        return BadRequest("Event Not Found");
      }

      if (await _context.Injausers.FindAsync(obj.PhotographerId) == null)
      {
        return BadRequest("Photographer Not Found");
      }

      if (await _context.Challengetypes.FindAsync(obj.ChallengeId) == null)
      {
        return BadRequest("Challenge Not Found");
      }

      #endregion

      var base64Data = Regex.Match(obj.Photo64string, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
      var bytes = Convert.FromBase64String(base64Data);

      var ct = DateTime.Now;
      var fileNameStorage = GetFileName(obj.EventId, obj.ChallengeId, obj.DivisionId, obj.ContenderId, obj.PhotographerId) + $"_{ct.Year}_{ct.Month}_{ct.Day}_{ct.Hour}_{ct.Minute}_{ct.Second}";
      var photourl = GetPhotoURL(obj.EventId, obj.ChallengeId, obj.DivisionId, obj.ContenderId, obj.PhotographerId);

      var dbPhoto = new Photo
      {
        Challengeid = obj.ChallengeId,
        Contenderid = obj.ContenderId,
        Divisionid = obj.DivisionId,
        Photographerid = obj.PhotographerId,
        Eventid = obj.EventId,
        Created = ct,
        StoredFileName = fileNameStorage,
        Filename = "base64",
        PhotoUrl = photourl
      };

      _context.Photos.Add(dbPhoto);

      await _context.SaveChangesAsync();

      await using (var stream = new FileStream(Path.Combine("./Photos", fileNameStorage), FileMode.Create))
      {
        await stream.WriteAsync(bytes);
      }

      return Ok(photourl);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error Saving Photo");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error decoding Image. See Internal Logs");
    }
  }
}