using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Oib).HasMaxLength(11);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.Address).HasMaxLength(255);
            builder.Property(u => u.PhoneNumber).HasMaxLength(20);
            builder.Property(u => u.DriversLicenceNumber).HasMaxLength(50);

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(u => u.Verified).IsRequired();
            builder.Property(u => u.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()"); // za SQL Server
        }
    }
}
