using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(v => v.Year)
                .IsRequired();

            builder.Property(v => v.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(v => v.ChassisNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.FuelType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.DoorsNumber).IsRequired();
            builder.Property(v => v.SeatsNumber).IsRequired();
            builder.Property(v => v.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(v => v.Owner)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
