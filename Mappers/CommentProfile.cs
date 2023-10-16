    using System;
using AutoMapper;
using Tidsbanken_BackEnd.Data.DTOs.CommentDTOs;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Mappers
{
    // Definition of the CommentProfile class, which inherits from AutoMapper's Profile class
    public class CommentProfile : Profile
	{
		public CommentProfile()
		{
            // CreateMap method to define bidirectional mapping between Comment and CommentPostDTO
            CreateMap<Comment, CommentDTO>()
                .ForMember(cdto => cdto.UserName, option => option
                .MapFrom(c => c.VacationRequest.User.FirstName))
                .ReverseMap();

            // CreateMap method to define bidirectional mapping between Comment and CommentPostDTO
            CreateMap<Comment, CommentPostDTO>().ReverseMap();

            // CreateMap method to define bidirectional mapping between Comment and CommentPutDTO
            CreateMap<Comment, CommentPutDTO>().ReverseMap();
        }
	}
}