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
        Content = $"The email '{dbItem.Email}' has a active Token '{dbItem.Token}' valid until '{dbItem.ValidUntilDate}'"
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
    
    var client = new BrevoClient(Helper.BrevoKey)
    {
      LogResponses = true,
      LogRequests = true,
      Logger = LogSendEmail
    };
    
    _context.Recoveremails.Add(dbItem);

    try
    {
      Serilog.Log.Information("Saving new Token {Token} valid until:{Valid}", dbItem.Token, dbItem.ValidUntilDate);
      await _context.SaveChangesAsync();

      var newUrl = new Uri(QueryHelpers.AddQueryString(
        Helper.ChangePasswordURL,
        new Dictionary<string, string> { { "token", dbItem.Token }, { "name", Convert.ToString(dbUser.Name)! } }!));

      Serilog.Log.Information("Sending Recovery Email to {Person}, Email:{Email}", dbUser.Name, dbUser.Mail);
      var result = await client.SendAsync(
        new Sender(Helper.MailNameFrom, Helper.MailAddressFrom),
        new List<Recipient> { new Recipient(dbUser.Name, email) },
        $"Password change order at BeautyComp placed on {DateTime.Now:U}",
        $"<html><h1>To change your password, please follow the next <a href=\"{newUrl}\">link</a></h1>" +
        $"<br><h3>This link will be valid until {DateTime.Now.AddHours(4):U}</h3>" +
        "</html>",
        true
      );
      Serilog.Log.Information("Result of seding mail {Person}", result);
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Internal Error Sending Email or Saving token");
      return Problem(e.ToString(), null, 500);
    }

    return Ok();
  }

  private void LogSendEmail(string aMessage)
  {
    Serilog.Log.Information("From Brevo: {aMessage}", aMessage);
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