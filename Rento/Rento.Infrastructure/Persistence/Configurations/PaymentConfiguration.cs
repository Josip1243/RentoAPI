using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.PaymentMethod)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.HasOne(p => p.Reservation)
                .WithMany()
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Restrict); // keep payments even if reservation is deleted
        }
    }
}
