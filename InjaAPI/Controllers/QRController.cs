using AutoMapper;
using InjaData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InjaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
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
	}
}
