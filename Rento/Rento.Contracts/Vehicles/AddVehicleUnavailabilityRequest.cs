namespace Rento.Contracts.Vehicles
{
    public class AddVehicleUnavailabilityRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }
}
