namespace Rento.Application.Vehicles.Common
{
    public class VehicleUnavailabilityDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
    }

}
