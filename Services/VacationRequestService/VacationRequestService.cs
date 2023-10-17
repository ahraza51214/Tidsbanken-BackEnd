using System;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Data.Enums;
using Tidsbanken_BackEnd.Exceptions;

namespace Tidsbanken_BackEnd.Services.VacationRequestService
{
	public class VacationRequestService : IVacationRequestService
	{
        // Database context for VacationRequest-related methods.
        private readonly TidsbankenDbContext _context;

        // Constructor to initialize the VacationRequestService with TidsbankenDbContext.
        public VacationRequestService(TidsbankenDbContext context)
        {
            _context = context;
        }

        // Get all VacationRequest asynchronously.
        public async Task<IEnumerable<VacationRequest>> GetAllAsync()
        {
            return await _context.VacationRequests.ToListAsync();
        }


        // Get a VacationRequest by its ID asynchronously.
        public async Task<VacationRequest?> GetByIdAsync(int id)
        {
            var VacationRequest = await _context.VacationRequests.Where(v => v.Id == id).FirstAsync();
            if (VacationRequest is null)
            {
                // Throw an exception if the VacationRequest with the specified ID is not found.
                throw new VacationRequestNotFoundException(id);
            }
            return VacationRequest;
        }


        // Update a VacationRequest asynchronously.
        public async Task<VacationRequest> UpdateAsync(VacationRequest obj)
        {
            // Check if the VacationRequest with the given ID exists.
            if (!await VacationRequestExists(obj.Id))
            {
                // Throw an Exception if the VacationRequest is not found.
                throw new VacationRequestNotFoundException(obj.Id);
            }

            // Mark the VacationRequest entity as modified and save changes to the database.
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }


        // Add a new VacationRequest asynchronously.
        public async Task<VacationRequest> AddAsync(VacationRequest obj)
        {
            // Add the VacationRequest to the database and save changes.
            obj.Status = VacationRequestStatus.Pending;
            await _context.VacationRequests.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }


        // Delete a VacationRequest by ID asynchronously.
        public async Task DeleteAsync(int id)
        {
            // Check if the VacationRequest with the given ID exists.
            if (!await VacationRequestExists(id))
            {
                // Throw an exception if the VacationRequest is not found.
                throw new VacationRequestNotFoundException(id);
            }
            // Find and remove the VacationRequest from the database.
            var vacationRequest = await _context.VacationRequests.FindAsync(id);
            _context.VacationRequests.Remove(vacationRequest);
            await _context.SaveChangesAsync();
        }


        // Check if a VacationRequest with a given ID exists in the database.
        private async Task<bool> VacationRequestExists(int id)
        {
            return await _context.VacationRequests.AnyAsync(v => v.Id == id);
        }
    }
}