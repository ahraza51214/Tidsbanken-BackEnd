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
    public class VacationRequestController : ControllerBase
    {
        private readonly TidsbankenDbContext _context;

        public VacationRequestController(TidsbankenDbContext context)
        {
            _context = context;
        }

        // GET: api/VacationRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VacationRequest>>> GetVacationRequests()
        {
          if (_context.VacationRequests == null)
          {
              return NotFound();
          }
            return await _context.VacationRequests.ToListAsync();
        }

        // GET: api/VacationRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VacationRequest>> GetVacationRequest(int id)
        {
          if (_context.VacationRequests == null)
          {
              return NotFound();
          }
            var vacationRequest = await _context.VacationRequests.FindAsync(id);

            if (vacationRequest == null)
            {
                return NotFound();
            }

            return vacationRequest;
        }

        // PUT: api/VacationRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVacationRequest(int id, VacationRequest vacationRequest)
        {
            if (id != vacationRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(vacationRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacationRequestExists(id))
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

        // POST: api/VacationRequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VacationRequest>> PostVacationRequest(VacationRequest vacationRequest)
        {
          if (_context.VacationRequests == null)
          {
              return Problem("Entity set 'TidsbankenDbContext.VacationRequests'  is null.");
          }
            _context.VacationRequests.Add(vacationRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVacationRequest", new { id = vacationRequest.Id }, vacationRequest);
        }

        // DELETE: api/VacationRequest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVacationRequest(int id)
        {
            if (_context.VacationRequests == null)
            {
                return NotFound();
            }
            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            _context.VacationRequests.Remove(vacationRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VacationRequestExists(int id)
        {
            return (_context.VacationRequests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
