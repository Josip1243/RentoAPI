namespace Rento.Application.Admin.Common
{
    public class AdminReviewResponse
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string VehicleName { get; set; } = default!;
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; } = default!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
