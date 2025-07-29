using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReservationDate).IsRequired();
            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();
            builder.Property(r => r.Status).IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // keep reservations even if user is deleted

            builder.HasOne(r => r.Vehicle)
                .WithMany(v => v.Reservations)
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Restrict); // keep reservations even if vehicle is deleted
        }
    }
}
