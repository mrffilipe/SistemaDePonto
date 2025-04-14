using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Domain.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<List<TimeEntry>> GetByDateAsync(Guid userId, DateTime startDate, DateTime? endDate = null);
        Task AddAsync(TimeEntry entry);
    }
}
