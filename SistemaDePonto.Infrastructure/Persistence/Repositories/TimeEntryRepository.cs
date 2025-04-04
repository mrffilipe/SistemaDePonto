using Microsoft.EntityFrameworkCore;
using SistemaDePonto.Domain.Entities;
using SistemaDePonto.Domain.Interfaces;

namespace SistemaDePonto.Infrastructure.Persistence.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeEntry>> GetByDateAsync(Guid userId, DateTime startDate, DateTime? endDate = null)
        {
            var query = _context.TimeEntries
               .Where(te => te.UserId == userId && te.Timestamp >= startDate);

            if (endDate.HasValue)
            {
                query = query.Where(te => te.Timestamp <= endDate.Value);
            }

            return await query
                .OrderBy(te => te.Timestamp)
                .ToListAsync();
        }

        public async Task AddAsync(TimeEntry entry)
        {
            _context.TimeEntries.Add(entry);
            await _context.SaveChangesAsync();
        }
    }
}
