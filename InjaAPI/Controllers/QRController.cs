using AutoMapper;
using InjaData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;

namespace InjaAPI.Controllers;

[Route("api/[controller]")]
[ApiController, Authorize]
public class QRController : ControllerBase
{
  private readonly dbContext _context;
  private readonly IMapper _mapper;
  private readonly TokenService _tokenService;

  public QRController(dbContext context, IMapper mapper, TokenService tokenService)
  {
    _context = context;
    _mapper = mapper;
    _tokenService = tokenService;
  }

  [AllowAnonymous]
  [HttpGet]
  public IActionResult GetQRCode(string data)
  {
    try
    {
      var qrGenerator = new QRCodeGenerator();
      var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
      var qrCode = new QRCode(qrCodeData);
      var qrCodeImage = qrCode.GetGraphic(20);

#pragma warning disable CA1416
#pragma warning disable CS8600
      var result = (byte[])new ImageConverter().ConvertTo(qrCodeImage, typeof(byte[]));
#pragma warning restore CS8600
#pragma warning restore CA1416

#pragma warning disable CS8604
      return File(result, "image/png");
#pragma warning restore CS8604
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
    }
  }
}