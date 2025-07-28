namespace Rento.Domain.Entities
{
    public class VehicleUnavailability
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }

        public Vehicle Vehicle { get; set; } = default!;
    }
}
