using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.CommentService
{
	public interface ICommentService : ICrudService<Comment, int>
	{
	}
}