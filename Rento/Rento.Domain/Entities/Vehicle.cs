namespace Rento.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = default!;
        public string ChassisNumber { get; set; } = default!;
        public string FuelType { get; set; } = default!;
        public int DoorsNumber { get; set; }
        public int SeatsNumber { get; set; }
        public decimal Price { get; set; }
        public int OwnerId { get; set; }

        // Navigacije
        public User Owner { get; set; } = default!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<VehicleImage> Images { get; set; } = new List<VehicleImage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<VehicleUnavailability> Unavailabilities { get; set; } = new List<VehicleUnavailability>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
