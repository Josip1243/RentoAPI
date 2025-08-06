namespace Rento.Contracts.Admin
{
    public class UpdateVehicleAdminRequest
    {
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int Year { get; set; }
        public string RegistrationNumber { get; set; } = default!;
        public string ChassisNumber { get; set; } = default!;
        public decimal Price { get; set; }
    }

}
