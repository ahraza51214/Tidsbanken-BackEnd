using System;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;

namespace Tidsbanken_BackEnd.Services.IneligiblePeriodService
{
	public class IneligiblePeriodService : IIneligiblePeriodService
	{
        // Database context for IneligiblePeriod-related methods.
        private readonly TidsbankenDbContext _context;

        // Constructor to initialize the IneligiblePeriodService with TidsbankenDbContext.
        public IneligiblePeriodService(TidsbankenDbContext context)
        {
            _context = context;
        }

        // Get all IneligiblePeriod asynchronously.
        public async Task<IEnumerable<IneligiblePeriod>> GetAllAsync()
        {
            return await _context.IneligiblePeriods.Include(i => i.User).ToListAsync();
        }


        // Get a IneligiblePeriod by its ID asynchronously.
        public async Task<IneligiblePeriod?> GetByIdAsync(int id)
        {
            // Check if the IneligiblePeriod with the given ID exists.
            if (!await IneligiblePeriodExists(id))
            {
                // Throw an exception if the IneligiblePeriod is not found.
                throw new IneligiblePeriodNotFoundException(id);
            }
            var ineligiblePeriod = await _context.IneligiblePeriods.Where(i => i.Id == id).Include(i => i.User).FirstAsync();
            return ineligiblePeriod;
        }


        // Update a IneligiblePeriod asynchronously.
        public async Task<IneligiblePeriod> UpdateAsync(IneligiblePeriod obj)
        {
            // Check if the IneligiblePeriod with the given ID exists.
            if (!await IneligiblePeriodExists(obj.Id))
            {
                // Throw an Exception if the IneligiblePeriod is not found.
                throw new IneligiblePeriodNotFoundException(obj.Id);
            }

            // Mark the IneligiblePeriod entity as modified and save changes to the database.
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }


        // Add a new IneligiblePeriod asynchronously.
        public async Task<IneligiblePeriod> AddAsync(IneligiblePeriod obj)
        {
            // Add the IneligiblePeriod to the database and save changes.
            await _context.IneligiblePeriods.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }


        // Delete a IneligiblePeriod by ID asynchronously.
        public async Task DeleteAsync(int id)
        {
            // Check if the IneligiblePeriod with the given ID exists.
            if (!await IneligiblePeriodExists(id))
            {
                // Throw an exception if the IneligiblePeriod is not found.
                throw new IneligiblePeriodNotFoundException(id);
            }
            // Find and remove the IneligiblePeriod from the database.
            var ineligiblePeriod = await _context.IneligiblePeriods.FindAsync(id);
            _context.IneligiblePeriods.Remove(ineligiblePeriod);
            await _context.SaveChangesAsync();
        }


        // Check if a IneligiblePeriod with a given ID exists in the database.
        private async Task<bool> IneligiblePeriodExists(int id)
        {
            return await _context.IneligiblePeriods.AnyAsync(i => i.Id == id);
        }
    }
}