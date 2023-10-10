using System;
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
        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }


        // Get a Comment by its ID asynchronously.
        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Where(c => c.Id == id).FirstAsync();
            if (comment is null)
            {
                // Throw an exception if the Comment with the specified ID is not found.
                throw new CommentNotFoundException(id);
            }
            return comment;
        }


        // Update a Comment asynchronously.
        public async Task<Comment> UpdateAsync(Comment obj)
        {
            // Check if the Comment with the given ID exists.
            if (!await CommentExists(obj.Id))
            {
                // Throw an Exception if the Comment is not found.
                throw new CommentNotFoundException(obj.Id);
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
        public async Task DeleteAsync(int id)
        {
            // Check if the Comment with the given ID exists.
            if (!await CommentExists(id))
            {
                // Throw an exception if the Comment is not found.
                throw new CommentNotFoundException(id);
            }
            // Find and remove the Comment from the database.
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }


        // Check if a Comment with a given ID exists in the database.
        private async Task<bool> CommentExists(int id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }

        // AddAsync method from the ICrudService which is not implemented.
        // Comment is always made in relation to a VacationRequest
        public Task<Comment> AddAsync(Comment obj)
        {
            throw new NotImplementedException();
        }
    }
}