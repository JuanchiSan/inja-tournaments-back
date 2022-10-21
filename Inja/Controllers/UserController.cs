using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;

namespace Inja.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly dbContext _context;

		public UserController(dbContext context)
		{
			_context = context;
		}

		// GET: api/User
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
		{
			return await _context.People.ToListAsync();
		}

		// GET: api/UserByEmail/jperez@pepe
		[HttpGet("/api/UserByEmail/{email}")]
		public async Task<ActionResult<Person>> GetPersonByEmail(string email)
		{
			var person =
				await _context.People.FirstOrDefaultAsync(x => x.Mail.ToLowerInvariant().Equals(email.ToLowerInvariant()));

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Person>> GetPerson(int id)
		{
			var person = await _context.People.FindAsync(id);

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		// PUT: api/User/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPerson(int id, Person person)
		{
			if (id != person.Userid)
			{
				return BadRequest();
			}

			_context.Entry(person).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PersonExists(id))
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

		// POST: api/User
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Person>> PostPerson(Person person)
		{
			_context.People.Add(person);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetPerson", new { id = person.Userid }, person);
		}

		// DELETE: api/User/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePerson(int id)
		{
			var person = await _context.People.FindAsync(id);
			if (person == null)
			{
				return NotFound();
			}

			_context.People.Remove(person);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool PersonExists(int id)
		{
			return _context.People.Any(e => e.Userid == id);
		}
	}
}
