using Rento.Domain.Enums;

namespace Rento.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigacije
        public User User { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<OwnerPayout> OwnerPayouts { get; set; } = new List<OwnerPayout>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
