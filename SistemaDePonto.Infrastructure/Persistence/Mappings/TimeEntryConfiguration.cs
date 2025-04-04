using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Infrastructure.Persistence
{
    public abstract class TimeEntryConfiguration : BaseEntityConfiguration<TimeEntry>
    {
        public override void Configure(EntityTypeBuilder<TimeEntry> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.TimeEntries)
                .HasForeignKey(x => x.UserId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
