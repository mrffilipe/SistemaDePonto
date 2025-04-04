using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaDePonto.Domain.Entities;

namespace SistemaDePonto.Infrastructure.Persistence
{
    public abstract class UserConfiguratio : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirebaseUid)
                .IsRequired();

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
