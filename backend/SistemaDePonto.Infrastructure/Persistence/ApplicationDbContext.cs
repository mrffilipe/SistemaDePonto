using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.SetCreatedAt();
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.SetUpdatedAt();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
