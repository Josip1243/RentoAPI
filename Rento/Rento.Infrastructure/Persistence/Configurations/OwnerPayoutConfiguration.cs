using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class OwnerPayoutConfiguration : IEntityTypeConfiguration<OwnerPayout>
    {
        public void Configure(EntityTypeBuilder<OwnerPayout> builder)
        {
            builder.ToTable("OwnerPayouts");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PayoutDate)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // keep payouts even if user is deleted

            builder.HasOne(p => p.Reservation)
                .WithMany()
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict); // keep payouts even if reservation is deleted
        }
    }
}
