using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.VacationRequestService
{
	public class VacationRequestService : IVacationRequestService
	{
		public VacationRequestService()
		{
		}

        public Task<VacationRequest> AddAsync(VacationRequest obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VacationRequest>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VacationRequest?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VacationRequest> UpdateAsync(VacationRequest obj)
        {
            throw new NotImplementedException();
        }
    }
}