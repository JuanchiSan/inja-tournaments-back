//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using InjaData.Models;

//namespace InjaAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CompetitionsController : ControllerBase
//    {
//        private readonly dbContext _context;

//        public CompetitionsController(dbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Competitions
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
//        {
//            return await _context.Competitions.ToListAsync();
//        }

//        // GET: api/Competitions/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Competition>> GetCompetition(int id)
//        {
//            var competition = await _context.Competitions.FindAsync(id);

//            if (competition == null)
//            {
//                return NotFound();
//            }

//            return competition;
//        }

//        // PUT: api/Competitions/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPut("{id}")]
//        //public async Task<IActionResult> PutCompetition(int id, Competition competition)
//        //{
//        //    if (id != competition.Idcompetition)
//        //    {
//        //        return BadRequest();
//        //    }

//        //    _context.Entry(competition).State = EntityState.Modified;

//        //    try
//        //    {
//        //        await _context.SaveChangesAsync();
//        //    }
//        //    catch (DbUpdateConcurrencyException)
//        //    {
//        //        if (!CompetitionExists(id))
//        //        {
//        //            return NotFound();
//        //        }
//        //        else
//        //        {
//        //            throw;
//        //        }
//        //    }

//        //    return NoContent();
//        //}

//        //// POST: api/Competitions
//        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        //[HttpPost]
//        //public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
//        //{
//        //    _context.Competitions.Add(competition);
//        //    await _context.SaveChangesAsync();

//        //    return CreatedAtAction("GetCompetition", new { id = competition.Idcompetition }, competition);
//        //}

//        //// DELETE: api/Competitions/5
//        //[HttpDelete("{id}")]
//        //public async Task<IActionResult> DeleteCompetition(int id)
//        //{
//        //    var competition = await _context.Competitions.FindAsync(id);
//        //    if (competition == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    _context.Competitions.Remove(competition);
//        //    await _context.SaveChangesAsync();

//        //    return NoContent();
//        //}

//        //private bool CompetitionExists(int id)
//        //{
//        //    return _context.Competitions.Any(e => e.Idcompetition == id);
//        //}
//    }
//}
