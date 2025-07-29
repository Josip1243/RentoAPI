using Microsoft.EntityFrameworkCore;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<OwnerPayout> OwnerPayouts { get; set; } = default!;
        public DbSet<UserPayoutAccount> UserPayoutAccounts { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Favorite> Favorites { get; set; } = default!;
        public DbSet<VehicleImage> VehicleImages { get; set; } = default!;
        public DbSet<VehicleUnavailability> VehicleUnavailabilities { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // TODO 
            // Remove this when you implement the actual configurations for your entities
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
                    {
                        foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                    }
        }
    }
}
