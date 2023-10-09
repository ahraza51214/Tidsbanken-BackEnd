using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.UserService
{
	public interface IUserService : ICrudService<User, int>
	{
	}
}