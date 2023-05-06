using Microsoft.AspNetCore.Mvc;
using InjaData.Models;
using InjaDTO;
using AutoMapper;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inja.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
public class PhotographerController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;

	public PhotographerController(dbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet("GetImageByDTO")]
	public IActionResult GetImage(ImageUploadDTO obj)
	{
		try
		{
			if (obj == null)
				return BadRequest("No info received");

			var imagePath = Path.Combine("./Photos", $"{obj.EventId}_{obj.ChallengeId}_{obj.ContenderId}_{obj.PhotographerId}.png");

			if (!System.IO.File.Exists(imagePath))
			{
				return NotFound();
			}

			var imageBytes = System.IO.File.ReadAllBytes(imagePath);

			return File(imageBytes, "image/png");
		}
		catch (Exception e)
		{
			Serilog.Log.Error("Error Reading Photo", e);
			return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
		}
	}

	[HttpGet("GetImage")]
	public IActionResult GetImage(string? param)
	{
		try
		{
			if (param == null)
				return BadRequest("No Image received");

			var imagePath = Path.Combine("./Photos", param);

			if (!System.IO.File.Exists(imagePath))
			{
				return NotFound();
			}

			var imageBytes = System.IO.File.ReadAllBytes(imagePath);

			return File(imageBytes, "image/png"); 
		}
		catch (Exception e)
		{
			Serilog.Log.Error("Error Reading Photo", e);
			return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
		}
	}

	//[HttpGet("GetImages")]
	//public async Task<ActionResult<List<string>>> GetImages(ImageUploadDTO obj)
	//{
	//	try
	//	{
	//		if (obj == null)
	//			return BadRequest("No Image received");

	//		var imagePath = Path.Combine("./Photos", $"");

	//		if (!System.IO.File.Exists(imagePath))
	//		{
	//			return NotFound();
	//		}

	//		var imageBytes = System.IO.File.ReadAllBytes(imagePath);

	//		return File(imageBytes, "image/png");
	//	}
	//	catch (Exception e)
	//	{
	//		Serilog.Log.Error("Error Reading Photo", e);
	//		return StatusCode(StatusCodes.Status500InternalServerError, "Error reading Image. See Internal Logs");
	//	}
	//}

	[HttpPost]
	public async Task<ActionResult<string>> UploadImage(ImageUploadDTO obj)
	{
		if (obj == null || obj.Photo64string == null) { return BadRequest("No Image or bad DTO"); }

		try
		{
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

			var base64Data = Regex.Match(obj.Photo64string, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
			var bytes = Convert.FromBase64String(base64Data);

			var fileName = $"{obj.EventId}_{obj.ChallengeId}_{obj.ContenderId}_{obj.PhotographerId}.png";
			var filePath = Path.Combine("./Photos", fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
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
}
