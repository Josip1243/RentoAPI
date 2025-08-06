namespace Rento.Application.Admin.Common
{
    public class AdminReservationResponse
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = default!;
        public int UserId { get; set; }
        public string UserName { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = default!;
    }
}
