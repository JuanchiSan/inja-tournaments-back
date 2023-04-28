using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InjaData.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using InjaDTO;

namespace Inja.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctypesController : ControllerBase
{
	private readonly dbContext _context;
	private readonly IMapper _mapper;


	public DoctypesController(dbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	// GET: api/Doctypes
	[HttpGet]
	public async Task<ActionResult<IEnumerable<DoctypeDTO>>> GetDoctypes()
	{
		return await _context.Doctypes.ProjectTo<DoctypeDTO>(_mapper.ConfigurationProvider).ToListAsync();
	}

	// GET: api/Doctypes/5
	[HttpGet("{id}")]
	public async Task<ActionResult<DoctypeDTO>> GetDoctype(short id)
	{
		var doctype = await _context.Doctypes.FirstOrDefaultAsync(x => x.Id == id);

		if (doctype == null)
		{
			return NotFound();
		}

		return _mapper.Map<DoctypeDTO>(doctype);
	}

	// PUT: api/Doctypes/5
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	//[HttpPut("{id}")]
	//public async Task<IActionResult> PutDoctype(short id, Doctype doctype)
	//{
	//    if (id != doctype.Docid)
	//    {
	//        return BadRequest();
	//    }

	//    _context.Entry(doctype).State = EntityState.Modified;

	//    try
	//    {
	//        await _context.SaveChangesAsync();
	//    }
	//    catch (DbUpdateConcurrencyException)
	//    {
	//        if (!DoctypeExists(id))
	//        {
	//            return NotFound();
	//        }
	//        else
	//        {
	//            throw;
	//        }
	//    }

	//    return NoContent();
	//}

	//// POST: api/Doctypes
	//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	//[HttpPost]
	//public async Task<ActionResult<Doctype>> PostDoctype(Doctype doctype)
	//{
	//    _context.Doctypes.Add(doctype);
	//    try
	//    {
	//        await _context.SaveChangesAsync();
	//    }
	//    catch (DbUpdateException)
	//    {
	//        if (DoctypeExists(doctype.))
	//        {
	//            return Conflict();
	//        }
	//        else
	//        {
	//            throw;
	//        }
	//    }

	//    return CreatedAtAction("GetDoctype", new { id = doctype.Docid }, doctype);
	//}

	// DELETE: api/Doctypes/5
	//[HttpDelete("{id}")]
	//public async Task<IActionResult> DeleteDoctype(short id)
	//{
	//    var doctype = await _context.Doctypes.FindAsync(id);
	//    if (doctype == null)
	//    {
	//        return NotFound();
	//    }

	//    _context.Doctypes.Remove(doctype);
	//    await _context.SaveChangesAsync();

	//    return NoContent();
	//}

	//private bool DoctypeExists(short id)
	//{
	//    return _context.Doctypes.Any(e => e.Docid == id);
	//}
}
