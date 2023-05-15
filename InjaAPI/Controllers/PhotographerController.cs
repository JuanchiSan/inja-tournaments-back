﻿using Microsoft.AspNetCore.Mvc;
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

  // [HttpGet("GetImageByDTO")]
  // public IActionResult GetImage(ImageUploadDTO obj)
  // {
  //   try
  //   {
  //     if (obj == null)
  //       return BadRequest("No info received");
  //
  //     var imagePath = Path.Combine("./Photos", $"{obj.EventId}_{obj.ChallengeId}_{obj.ContenderId}_{obj.PhotographerId}.png");
  //
  //     if (!System.IO.File.Exists(imagePath))
  //     {
  //       return NotFound();
  //     }
  //
  //     var imageBytes = System.IO.File.ReadAllBytes(imagePath);
  //
  //     return File(imageBytes, "image/png");
  //   }
  //   catch (Exception e)
  //   {
  //     Serilog.Log.Error("Error Reading Photo", e);
  //     return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
  //   }
  // }

  // [HttpGet("GetImage")]
  // public IActionResult GetImage(string? param)
  // {
  //   try
  //   {
  //     if (param == null)
  //       return BadRequest("No Image received");
  //
  //     var imagePath = Path.Combine("./Photos", param);
  //
  //     if (!System.IO.File.Exists(imagePath))
  //     {
  //       return NotFound();
  //     }
  //
  //     var imageBytes = System.IO.File.ReadAllBytes(imagePath);
  //
  //     return File(imageBytes, "image/png");
  //   }
  //   catch (Exception e)
  //   {
  //     Serilog.Log.Error("Error Reading Photo", e);
  //     return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
  //   }
  // }

  [AllowAnonymous]
  [HttpGet("GetContenderChallenges")]
  public async Task<ActionResult<ResponseDivisionContenderChallengeDTO>> GetContenderChallenges(int eventid, int challengeid, int divisionid, int contenderid)
  {
    var dbItems = await _context
      .VUserinscriptionPlanas
      .Where(x => x.Eventid == eventid && x.Challengeid == challengeid)
      .ToListAsync();

    if (dbItems.All(x => x.Userid != contenderid)) return NotFound();

    var dbPhoto = _context
      .Photos
      .FirstOrDefault(x => x.Challengeid == challengeid && x.Eventid == eventid && x.Contenderid == contenderid);

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

  [AllowAnonymous]
  [HttpPost("UploadPhotoFile")]
  public async Task<IActionResult> UploadPhotoFile(int eventId, int challengeId, int divisionid, int contenderId, int photographerId, IFormFile? file)
  {
    try
    {
      if (file == null)
        return BadRequest("No Photo Received");

      var ct = DateTime.Now;

      if (file.Length <= 0) return BadRequest("File length is 0");
      var filenameResponse = $"{eventId}_{challengeId}_{divisionid}{contenderId}_{photographerId}";
      var fileNameStorage = filenameResponse + $"_{ct.Year}_{ct.Month}_{ct.Day}_{ct.Hour}_{ct.Minute}_{ct.Second}";
      var photourl =
        $"http://inja-api.guadcore.ar/api/Photographer/DownloadPhotoFile?eventId={eventId}&challengeId={challengeId}&divisionid={divisionid}&contenderId={contenderId}&photographerId={photographerId}";

      var dbPhoto = new Photo
      {
        Challengeid = challengeId,
        Contenderid = contenderId,
        Divisionid = divisionid,
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
      return Ok(photourl);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error Saving File");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  [AllowAnonymous]
  [HttpGet("DownloadPhotoFile")]
  public IActionResult DownloadPhotoFile(int eventId, int challengeId, int divisionId, int contenderId, int photographerId)
  {
    try
    {
      var dbItems = _context.Photos
        .Where(x => x.Divisionid == divisionId && x.Challengeid == challengeId && x.Eventid == eventId && x.Photographerid == photographerId && x.Contenderid == contenderId)
        .OrderByDescending(x => x.Created);
      if (!dbItems.Any())
        return NotFound("Image Not Found");

      var dbItem = dbItems.First();

      var filePath = Path.Combine("./Photos", dbItem.StoredFileName);

      return File(System.IO.File.OpenRead(filePath), "image/png", string.IsNullOrEmpty(dbItem.Filename) ? "Image.png" : dbItem.Filename);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error Reading File");
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }

  /*
  [HttpPost]
  public async Task<ActionResult<string>> UploadImage(ImageUploadDTO obj)
  {
    if (obj.Photo64string == null)
    {
      return BadRequest("No Image or bad DTO");
    }

    try
    {
      #region Controls

      if (_context.Injausers.Find(obj.ContenderId) == null)
      {
        return BadRequest("Contender Not Found");
      }

      if (_context.Events.Find(obj.EventId) == null)
      {
        return BadRequest("Event Not Found");
      }

      if (_context.Injausers.Find(obj.PhotographerId) == null)
      {
        return BadRequest("Photographer Not Found");
      }

      if (_context.Challengetypes.Find(obj.ChallengeId) == null)
      {
        return BadRequest("Challenge Not Found");
      }

      #endregion

      var base64Data = Regex.Match(obj.Photo64string, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
      var bytes = Convert.FromBase64String(base64Data);

      var fileName = $"{obj.EventId}_{obj.ChallengeId}_{obj.ContenderId}_{obj.PhotographerId}.png";
      var filePath = Path.Combine("./Photos", fileName);

      await using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await stream.WriteAsync(bytes);
      }

      return Ok($"http://inja-api.guadcore.ar/api/Photographer/GetImage?param={fileName}");
    }
    catch (Exception e)
    {
      Serilog.Log.Error("Error Saving Photo", e);
      return StatusCode(StatusCodes.Status500InternalServerError, "Error decoding Image. See Internal Logs");
    }
  }
  */
}