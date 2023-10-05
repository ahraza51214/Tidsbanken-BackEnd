using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.CommentService
{
	public class CommentService : ICommentService
	{
		public CommentService()
		{
		}

        public Task<Comment> AddAsync(Comment obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync(Comment obj)
        {
            throw new NotImplementedException();
        }
    }
}