using System;
using AutoMapper;
using Tidsbanken_BackEnd.Data.DTOs.UserDTOs;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Mappers
{
    // Definition of the UserProfile class, which inherits from AutoMapper's Profile class
    public class UserProfile : Profile
	{
		public UserProfile()
		{
            // CreateMap method to define bidirectional mapping between User and UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(udto => udto.RoleName, options => options
                .MapFrom(u => u.Role.RoleName)
                
                ).ReverseMap();

            // CreateMap method to define bidirectional mapping between User and UserPostDTO
            CreateMap<User, UserPostDTO>().ReverseMap();

            // CreateMap method to define bidirectional mapping between User and UserPutDTO
            CreateMap<User, UserPutDTO>().ReverseMap();
        }
	}
}