using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.CommentService
{
	public interface ICommentService
	{
        // AddAsync method with both Comment object and VacationRequest ID
        Task<Comment> AddAsync(Comment comment, int vacationRequestId);
        Task<Comment> GetByIdAsync(int id, int vacationRequestId);
        Task<IEnumerable<Comment>> GetAllAsync(int vacationRequestId);
        Task<Comment> UpdateAsync(Comment obj, int vacationRequestId);
        Task DeleteAsync(int id, int vacationRequestId);
    }
}