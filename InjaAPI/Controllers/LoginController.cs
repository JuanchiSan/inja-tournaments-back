using Microsoft.AspNetCore.Mvc;
using InjaData.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace InjaAPI.Controllers;

	[Route("api")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly dbContext _context;
		private readonly IMapper _mapper;
		private readonly TokenService _tokenService;

		public LoginController(dbContext context, IMapper mapper, TokenService tokenService)
		{
			_context = context;
			_mapper = mapper;
			_tokenService = tokenService;
		}

		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
		public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
		{
			Serilog.Log.Logger.Information("{CurrentMethod} from IP: {RemoteIpAddress}", 
				System.Reflection.MethodBase.GetCurrentMethod()?.Name, 
				Helper.GetRemoteIpAddress(HttpContext));

			if (!ModelState.IsValid)
			{
				Serilog.Log.Warning("Bad Login: {RequestEmail} {RequestPassword} {RequestEventId}", request.Email, request.Password, request.EventId);
				return BadRequest(ModelState);
			}

			var managedUser = await _context.Injausers.Include(x => x.Injauserusertypes).FirstOrDefaultAsync(x => x.Mail == request.Email && x.Injauserusertypes.Any(z => z.Eventid == request.EventId));
			if (managedUser == null)
			{
				Serilog.Log.Warning("Bad Login: {RequestEmail} {RequestPassword} {RequestEventId}", request.Email, request.Password, request.EventId);
				return BadRequest("Bad credentials");
			}
			
			var isPasswordValid = managedUser.Pass == request.Password;
			if (!isPasswordValid)
			{
				Serilog.Log.Warning("Bad Login: {RequestEmail} {RequestPassword} {RequestEventId}", request.Email, request.Password, request.EventId);
				return Unauthorized();
			}

			var accessToken = _tokenService.CreateToken(managedUser, request.EventId);
			await _context.SaveChangesAsync();
			Serilog.Log.Information("GOOD Login: {RequestEmail} {RequestPassword} {RequestEventId}", request.Email, request.Password, request.EventId);
			return Ok(new AuthResponse
			{
				Username = $"{managedUser.Lastname}, {managedUser.Firstname}",
				Email = managedUser.Mail,
				Token = accessToken,
			});
		}

		[HttpGet("testjwt")]
		[Authorize]
		public IActionResult TestJWT()
		{
			// Obtener el token JWT del encabezado de autorización
			string? token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

			if (string.IsNullOrWhiteSpace(token))
				return StatusCode(StatusCodes.Status500InternalServerError, "JWT Token Not valid");

			var tokenData = new JwtSecurityTokenHandler().ReadJwtToken(token);
			var jwtUserName = tokenData.Claims.FirstOrDefault(claim => claim.Type == Helper.JWTClaimUserName);
			if (jwtUserName == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "JWT Token Not valid");
			}

			return Ok($"Sos vos {jwtUserName.Value}?");
		}
	}
