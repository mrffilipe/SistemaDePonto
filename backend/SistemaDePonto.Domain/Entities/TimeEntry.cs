using SistemaDePonto.Domain.Enums;

namespace SistemaDePonto.Domain.Entities
{
    public class TimeEntry : BaseEntity
    {
        public Guid UserId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public EntryType Type { get; private set; }

        public User? User { get; private set; }

        protected TimeEntry() { }

        public TimeEntry(Guid userId, DateTime timestamp, EntryType type)
        {
            UserId = userId;
            Timestamp = timestamp;
            Type = type;
        }
    }
}