using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.VacationRequestService
{
	public interface IVacationRequestService : ICrudService<VacationRequest, int>
	{
        Task<IEnumerable<VacationRequest>> GetAllAsync(Guid id);
    }
}