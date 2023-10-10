using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.CommentService
{
	public interface ICommentService : ICrudService<Comment, int>
	{
        // AddAsync method with both Comment object and VacationRequest ID
        Task<Comment> AddAsync(Comment comment, int vacationRequestId);
    }
}