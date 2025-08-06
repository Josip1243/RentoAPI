namespace Rento.Application.Vehicles.Common
{
    public class AdminVehicleResponse
    {
        public int Id { get; set; }
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = default!;
        public string ChassisNumber { get; set; } = default!;
        public decimal Price { get; set; }

        public int OwnerId { get; set; }
        public string OwnerEmail { get; set; } = default!;
        public string OwnerName { get; set; } = default!;
    }
}
