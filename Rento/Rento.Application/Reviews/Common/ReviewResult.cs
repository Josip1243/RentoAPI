namespace Rento.Application.Reviews.Common
{
    public class ReviewResult
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int OwnerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ReviewerId { get; set; }
    }
}
