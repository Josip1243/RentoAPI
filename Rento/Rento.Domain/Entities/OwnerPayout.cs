using Rento.Domain.Enums;

namespace Rento.Domain.Entities
{
    public class OwnerPayout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayoutDate { get; set; }
        public PayoutStatus Status { get; set; }

        public User User { get; set; } = default!;
        public Reservation Reservation { get; set; } = default!;
    }
}
