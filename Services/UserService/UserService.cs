using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.UserService
{
	public class UserService : IUserService
	{
		public UserService()
		{
		}

        public Task<User> AddAsync(User obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User obj)
        {
            throw new NotImplementedException();
        }
    }
}