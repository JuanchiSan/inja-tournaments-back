using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using SendWithBrevo;

namespace InjaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecoverPassController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;

  public RecoverPassController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  [AllowAnonymous]
  [HttpGet("SendRecoveryEmail")]
  [ProducesResponseType(StatusCodes.Status200OK),
   ProducesResponseType(StatusCodes.Status208AlreadyReported),
   ProducesResponseType(StatusCodes.Status404NotFound),
   ProducesResponseType(StatusCodes.Status500InternalServerError)]
  [SuppressMessage("ReSharper.DPA", "DPA0007: Large number of DB records", MessageId = "count: 231")]
  public async Task<ActionResult> GenerateTokenForRecovery(string email)
  {
    email = email.ToLowerInvariant();
    var dbItems = await _context.Recoveremails.ToListAsync();
    var dbItem = dbItems.FirstOrDefault(x =>
      string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase) && x.UsedDate == null && x.ValidUntilDate > DateTime.Now);

    if (dbItem != null)
    {
      return new ContentResult
      {
        StatusCode = StatusCodes.Status208AlreadyReported,
        Content = dbItem.Token
      };
    }

    var dbUser = _context
      .Injausers
      .FirstOrDefault(x => x.Mail == email);
    
    if (dbUser == null)
    {
      return new ContentResult
      {
        StatusCode = StatusCodes.Status404NotFound,
        Content = "Email was not found in the system"
      };
    }
    
    dbItem = new Recoveremail
    {
      Email = email.ToLowerInvariant(),
      CreationDate = DateTime.Now,
      ValidUntilDate = DateTime.Now.AddHours(4),
      Token = Guid.NewGuid().ToString()
    };
    
    #region Send Email
    var client = new BrevoClient(Helper.BrevoKey);

    var newUrl = new Uri(QueryHelpers.AddQueryString(
      Helper.ChangePasswordURL,
      new Dictionary<string, string> { { "token", dbItem.Token }, { "name", Convert.ToString(dbUser.Name)! } }!));

    try
    {
      Serilog.Log.Information("Sending Recovery Email to {Person}, Email:{Email}", dbUser.Name, dbUser.Mail);
      await client.SendAsync(
        new Sender(Helper.MailNameFrom, Helper.MailAddressFrom),
        new List<Recipient> { new Recipient(dbUser.Name, email) },
        "Change Password from Beautycomp",
        $"<html><h1>To change your password, please follow the next <a href=\"{newUrl}\">link</a></h1></html>",
        true
      );
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e,"Error Sending Recovery Email to {Person}, Email:{Email}", dbUser.Name, dbUser.Mail);
      return Problem(e.ToString(), null, 500);
    }

    #endregion
    
    _context.Recoveremails.Add(dbItem);

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error saving Recovery Password");
      return Problem(e.ToString(), null, 500);
    }

    return Ok();
  }


  [AllowAnonymous]
  [HttpPost("ChangePassword")]
  [ProducesResponseType(StatusCodes.Status200OK),
   ProducesResponseType(StatusCodes.Status404NotFound),
   ProducesResponseType(StatusCodes.Status500InternalServerError)]
  public async Task<ActionResult> ChangeUserPassword(string token, string newPassword)
  {
    var dbToken = _context.Recoveremails
      .FirstOrDefault(x => x.UsedDate == null && x.Token == token && x.ValidUntilDate > DateTime.Now);

    if (dbToken == null)
    {
      return new ContentResult
      {
        StatusCode = StatusCodes.Status404NotFound,
        Content = "No Valid Token was found for this email or Token Expired"
      };
    }
    
    var dbUser = _context
      .Injausers
      .FirstOrDefault(x => x.Mail == dbToken.Email);
    
    if (dbUser == null)
    {
      return new ContentResult
      {
        StatusCode = StatusCodes.Status404NotFound,
        Content = "User not Found"
      };
    }
    
    dbToken.UsedDate = DateTime.Now;
    dbUser.Pass = newPassword;    
    
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error changing password");
      return Problem(e.ToString(), null, 500);
    }

    return Ok();
  }
}