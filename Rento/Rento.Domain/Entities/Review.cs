namespace Rento.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ReviewerId { get; set; }
        public int VehicleId { get; set; }
        public int OwnerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public Reservation Reservation { get; set; } = default!;
        public User Reviewer { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public User Owner { get; set; } = default!;
    }
}
