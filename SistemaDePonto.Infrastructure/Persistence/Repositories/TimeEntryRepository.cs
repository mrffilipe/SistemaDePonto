using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Persistence.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        public Task<List<TimeEntry>> GetByDateAsync(Guid userId, DateTime startDate, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TimeEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
