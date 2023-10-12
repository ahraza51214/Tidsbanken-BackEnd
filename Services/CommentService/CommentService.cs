using System;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;

namespace Tidsbanken_BackEnd.Services.CommentService
{
	public class CommentService : ICommentService
	{
        // Database context for Comment-related methods.
        private readonly TidsbankenDbContext _context;

        // Constructor to initialize the CommentService with TidsbankenDbContext.
        public CommentService(TidsbankenDbContext context)
        {
            _context = context;
        }


        // Get all Comment asynchronously.
        public async Task<IEnumerable<Comment>> GetAllAsync(int vacationRequestId)
        {
            return await _context.Comments.Where(c => c.VacationRequestId == vacationRequestId).Include(c => c.VacationRequest).ThenInclude(v => v.User).ToListAsync();
        }


        // Update a Comment asynchronously.
        public async Task<Comment> UpdateAsync(Comment obj, int vacationRequestId)
        {
            // Check if the Comment with the given ID exists.
            if (!await CommentExists(obj.Id))
            {
                // Throw an Exception if the Comment is not found.
                throw new CommentNotFoundException(obj.Id);
            }

            // Retrieve the associated VacationRequest and its current status
            var vacationRequest = await _context.VacationRequests
                .Where(vr => vr.Id == vacationRequestId)
                .FirstOrDefaultAsync();

            if (vacationRequest != null)
            {
                // Set the Status property of the Comment based on the VacationRequest's status
                obj.StatusAtTimeOfComment = vacationRequest.Status;
            }

            // Mark the Comment entity as modified and save changes to the database.
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }


        // Add a new Comment asynchronously.
        public async Task<Comment> AddAsync(Comment obj, int vacationRequestId)
        {
            // Retrieve the associated VacationRequest and its current status
            var vacationRequest = await _context.VacationRequests
                .Where(vr => vr.Id == vacationRequestId)
                .FirstOrDefaultAsync();

            if (vacationRequest != null)
            {
                // Set the Status property of the Comment based on the VacationRequest's status
                obj.StatusAtTimeOfComment = vacationRequest.Status;
            }

            // Add the Comment to the database and save changes.
            await _context.Comments.AddAsync(obj);
            await _context.SaveChangesAsync();

            return obj;
        }


        // Delete a Comment by ID asynchronously.
        public async Task DeleteAsync(int id, int vacationRequestId)
        {
            // Check if the Comment with the given ID exists.
            if (!await CommentExists(id))
            {
                // Throw an exception if the Comment is not found.
                throw new CommentNotFoundException(id);
            }

            // Find and remove the Comment from the database.
            var comment = await _context.Comments.FindAsync(id);

            if (comment.VacationRequestId != vacationRequestId)
            {
                throw new Exception($"Comment with ID {id} is not associated with VacationRequest ID {vacationRequestId}.");
            }
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }


        // Check if a Comment with a given ID exists in the database.
        private async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }
    }
}