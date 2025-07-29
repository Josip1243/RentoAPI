using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class VehicleImageConfiguration : IEntityTypeConfiguration<VehicleImage>
    {
        public void Configure(EntityTypeBuilder<VehicleImage> builder)
        {
            builder.ToTable("VehicleImages");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Url)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.Description)
                .HasMaxLength(500);

            builder.HasOne(i => i.Vehicle)
                .WithMany(v => v.Images)
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Cascade); // delete images when vehicle is deleted
        }
    }
}
