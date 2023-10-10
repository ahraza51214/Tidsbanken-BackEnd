using System;
using AutoMapper;
using Tidsbanken_BackEnd.Data.DTOs.VacationRequestDTOs;
using Tidsbanken_BackEnd.Data.Entities;
using Tidsbanken_BackEnd.Data.Enums;

namespace Tidsbanken_BackEnd.Mappers
{
    // Definition of the VacationRequestProfile class, which inherits from AutoMapper's Profile class
    public class VacationRequestProfile : Profile
	{
		public VacationRequestProfile()
		{
            // CreateMap method to define bidirectional mapping between VacationRequest and VacationRequestDTO
            CreateMap<VacationRequest, VacationRequestDTO>()
                .ForMember(vrdto => vrdto.Status, option => option
                .MapFrom(v => Array.IndexOf(Enum.GetValues(typeof(VacationRequestStatus)), v.Status)));

            // CreateMap method to define bidirectional mapping between VacationRequest and VacationRequestPostDTO
            CreateMap<VacationRequest, VacationRequestPostDTO>().ReverseMap();

            // CreateMap method to define bidirectional mapping between VacationRequest and VacationRequestPutDTO
            CreateMap<VacationRequest, VacationRequestPutDTO>().ReverseMap();
        }
	}
}