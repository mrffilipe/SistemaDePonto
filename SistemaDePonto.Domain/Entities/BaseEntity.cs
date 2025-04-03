namespace SistemaDePonto.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public void SetUpdatedAt(DateTime updatedAt)
        {
            UpdatedAt = updatedAt;
        }
    }
}
