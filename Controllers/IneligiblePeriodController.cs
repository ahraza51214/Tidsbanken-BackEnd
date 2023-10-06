using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IneligiblePeriodController : ControllerBase
    {
        private readonly TidsbankenDbContext _context;

        public IneligiblePeriodController(TidsbankenDbContext context)
        {
            _context = context;
        }

        // GET: api/IneligiblePeriod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IneligiblePeriod>>> GetIneligiblePeriods()
        {
          if (_context.IneligiblePeriods == null)
          {
              return NotFound();
          }
            return await _context.IneligiblePeriods.ToListAsync();
        }

        // GET: api/IneligiblePeriod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IneligiblePeriod>> GetIneligiblePeriod(int id)
        {
          if (_context.IneligiblePeriods == null)
          {
              return NotFound();
          }
            var ineligiblePeriod = await _context.IneligiblePeriods.FindAsync(id);

            if (ineligiblePeriod == null)
            {
                return NotFound();
            }

            return ineligiblePeriod;
        }

        // PUT: api/IneligiblePeriod/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIneligiblePeriod(int id, IneligiblePeriod ineligiblePeriod)
        {
            if (id != ineligiblePeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(ineligiblePeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IneligiblePeriodExists(id))
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

        // POST: api/IneligiblePeriod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IneligiblePeriod>> PostIneligiblePeriod(IneligiblePeriod ineligiblePeriod)
        {
          if (_context.IneligiblePeriods == null)
          {
              return Problem("Entity set 'TidsbankenDbContext.IneligiblePeriods'  is null.");
          }
            _context.IneligiblePeriods.Add(ineligiblePeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIneligiblePeriod", new { id = ineligiblePeriod.Id }, ineligiblePeriod);
        }

        // DELETE: api/IneligiblePeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIneligiblePeriod(int id)
        {
            if (_context.IneligiblePeriods == null)
            {
                return NotFound();
            }
            var ineligiblePeriod = await _context.IneligiblePeriods.FindAsync(id);
            if (ineligiblePeriod == null)
            {
                return NotFound();
            }

            _context.IneligiblePeriods.Remove(ineligiblePeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IneligiblePeriodExists(int id)
        {
            return (_context.IneligiblePeriods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
