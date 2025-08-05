using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.CreatedAt)
                .HasConversion(
                    v => v, // DateTime to DateTime conversion
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // ensure UTC kind for consistency
                )
                .IsRequired();

            builder.HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Vehicle)
                .WithMany(v => v.Favorites)
                .HasForeignKey(f => f.VehicleId)
                .OnDelete(DeleteBehavior.Cascade); // delete favorites when vehicle is deleted
        }
    }
}
