using System;
using Tidsbanken_BackEnd.Data.Entities;

namespace Tidsbanken_BackEnd.Services.IneligiblePeriodService
{
	public class IneligiblePeriodService : IIneligiblePeriodService
	{
		public IneligiblePeriodService()
		{
		}

        public Task<IneligiblePeriod> AddAsync(IneligiblePeriod obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IneligiblePeriod>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IneligiblePeriod?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IneligiblePeriod> UpdateAsync(IneligiblePeriod obj)
        {
            throw new NotImplementedException();
        }
    }
}