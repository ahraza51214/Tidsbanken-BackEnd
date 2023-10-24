using System;
using System.Threading.Tasks;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.UserService
{
	public interface IUserService : ICrudService<User, int>
	{
        // Asynchronously get a User entity by its Guid type Id.
        Task<User> GetByIdAsync(Guid id);

        // Asynchronously delete a User entity by its Guid type Id.
        Task DeleteAsync(Guid id);
    }
}