using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;

namespace Inja.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CitiesController : ControllerBase
	{
		private readonly dbContext _context;

		public CitiesController(dbContext context)
		{
			_context = context;
		}

		// GET: api/Cities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<City>>> GetCities()
		{
			return await _context.Cities.ToListAsync();
		}

		[HttpGet("/api/GetCitiesByName/{aCityName}")]
		public async Task<ActionResult<List<City>>> GetCitiesByName(string aCityName)
		{
			var lstCities = await _context.Cities.ToListAsync();
			lstCities = lstCities.Where(x => x.Cityname.ToLowerInvariant().Contains(aCityName.ToLowerInvariant())).ToList();

			if (lstCities.Count == 0)
			{
				return NotFound();
			}

			return lstCities;
		}

		// GET: api/Cities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<City>> GetCity(int id)
		{
			var city = await _context.Cities.FindAsync(id);

			if (city == null)
			{
				return NotFound();
			}

			return city;
		}

		// PUT: api/Cities/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCity(int id, City city)
		{
			if (id != city.Idcity)
			{
				return BadRequest();
			}

			_context.Entry(city).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CityExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Cities
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<City>> PostCity(City city)
		{
			_context.Cities.Add(city);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCity", new { id = city.Idcity }, city);
		}

		// DELETE: api/Cities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCity(int id)
		{
			var city = await _context.Cities.FindAsync(id);
			if (city == null)
			{
				return NotFound();
			}

			_context.Cities.Remove(city);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CityExists(int id)
		{
			return _context.Cities.Any(e => e.Idcity == id);
		}
	}
}
