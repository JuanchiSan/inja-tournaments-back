using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;

namespace Inja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctypesController : ControllerBase
    {
        private readonly dbContext _context;

        public DoctypesController(dbContext context)
        {
            _context = context;
        }

        // GET: api/Doctypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctype>>> GetDoctypes()
        {
            return await _context.Doctypes.ToListAsync();
        }

        // GET: api/Doctypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctype>> GetDoctype(short id)
        {
            var doctype = await _context.Doctypes.FindAsync(id);

            if (doctype == null)
            {
                return NotFound();
            }

            return doctype;
        }

        // PUT: api/Doctypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctype(short id, Doctype doctype)
        {
            if (id != doctype.Docid)
            {
                return BadRequest();
            }

            _context.Entry(doctype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctypeExists(id))
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

        // POST: api/Doctypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Doctype>> PostDoctype(Doctype doctype)
        {
            _context.Doctypes.Add(doctype);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DoctypeExists(doctype.Docid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDoctype", new { id = doctype.Docid }, doctype);
        }

        // DELETE: api/Doctypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctype(short id)
        {
            var doctype = await _context.Doctypes.FindAsync(id);
            if (doctype == null)
            {
                return NotFound();
            }

            _context.Doctypes.Remove(doctype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DoctypeExists(short id)
        {
            return _context.Doctypes.Any(e => e.Docid == id);
        }
    }
}
