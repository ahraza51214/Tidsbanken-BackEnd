using System;
using AutoMapper;
using Tidsbanken_BackEnd.Data.DTOs.IneligiblePeriodDTOs;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Mappers
{
    // Definition of the IneligiblePeriodProfile class, which inherits from AutoMapper's Profile class
    public class IneligiblePeriodProfile : Profile
	{
		public IneligiblePeriodProfile()
		{
            // CreateMap method to define bidirectional mapping between IneligiblePeriod and IneligiblePeriodDTO
            CreateMap<IneligiblePeriod, IneligiblePeriodDTO>()
                .ForMember(idto => idto.ManagerName, option => option
                .MapFrom(i => i.User.FirstName))
                .ReverseMap();

            // CreateMap method to define bidirectional mapping between IneligiblePeriod and IneligiblePeriodPostDTO
            CreateMap<IneligiblePeriod, IneligiblePeriodPostDTO>().ReverseMap();

            // CreateMap method to define bidirectional mapping between IneligiblePeriod and IneligiblePeriodPutDTO
            CreateMap<IneligiblePeriod, IneligiblePeriodPutDTO>().ReverseMap();
        }
	}
}