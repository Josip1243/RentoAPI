using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class VehicleUnavailabilityConfiguration : IEntityTypeConfiguration<VehicleUnavailability>
    {
        public void Configure(EntityTypeBuilder<VehicleUnavailability> builder)
        {
            builder.ToTable("VehicleUnavailabilities");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.StartDate)
                .IsRequired();

            builder.Property(u => u.EndDate)
                .IsRequired();

            builder.Property(u => u.Reason)
                .HasMaxLength(500);

            builder.HasOne(u => u.Vehicle)
                .WithMany(v => v.Unavailabilities)
                .HasForeignKey(u => u.VehicleId)
                .OnDelete(DeleteBehavior.Cascade); // delete unavailabilities when vehicle is deleted
        }
    }
}
