namespace SistemaDePonto.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void SetCreatedAt()
        {
            var now = DateTime.UtcNow;
            CreatedAt = now;
            UpdatedAt = now;
        }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
