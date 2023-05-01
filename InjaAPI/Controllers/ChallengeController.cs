using Microsoft.AspNetCore.Mvc;
using InjaData.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace InjaAPI.Controllers
{
	[Route("api/[controller]"), Authorize]
	[ApiController]
	public class ChallengeController : ControllerBase
	{
		private readonly dbContext _context;
		private readonly IMapper _mapper;

		public ChallengeController(dbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
	}
}
