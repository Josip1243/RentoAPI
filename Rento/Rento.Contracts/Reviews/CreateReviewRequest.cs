namespace Rento.Contracts.Reviews
{
    public class CreateReviewRequest
    {
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
