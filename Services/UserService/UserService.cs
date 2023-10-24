using System;
using Microsoft.EntityFrameworkCore;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Exceptions;

namespace Tidsbanken_BackEnd.Services.UserService
{
	public class UserService : IUserService
	{
        // Database context for User-related methods.
        private readonly TidsbankenDbContext _context;

        // Constructor to initialize the UserService with TidsbankenDbContext.
        public UserService(TidsbankenDbContext context)
        {
            _context = context;
        }

        // Get all Users asynchronously.
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }


        // Get a User by its ID asynchronously.
        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstAsync();
            if (user is null)
            {
                // Throw an exception if the User with the specified ID is not found.
                throw new UserNotFoundException(id);
            }
            return user;
        }


        // Update a User asynchronously.
        public async Task<User> UpdateAsync(User obj)
        {
            // Check if the User with the given ID exists.
            if (!await UserExists(obj.Id))
            {
                // Throw an exception if the User is not found.
                throw new UserNotFoundException(obj.Id);
            }

            // Mark the User entity as modified and save changes to the database.
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }


        // Add a new User asynchronously.
        public async Task<User> AddAsync(User obj)
        {
            // Add the User to the database and save changes.
            await _context.Users.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }


        // Delete a User by ID asynchronously.
        public async Task DeleteAsync(Guid id)
        {
            // Check if the User with the given ID exists.
            if (!await UserExists(id))
            {
                // Throw an exception if the User is not found.
                throw new UserNotFoundException(id);
            }
            // Find and remove the User from the database.
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


        // Check if a User with a given ID exists in the database.
        private async Task<bool> UserExists(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }


        // Signature method from ICrudService, not implemented becuase we use Guid type for Id in User and not int
        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        // Signature method from ICrudService, not implemented becuase we use Guid type for Id in User and not int
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}