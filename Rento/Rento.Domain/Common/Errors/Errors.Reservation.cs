using ErrorOr;

namespace Rento.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Reservation
        {
            public static Error ReservationNotFound => Error.NotFound(
                code: "Reservation.ReservationNotFound",
                description: "Reservation is not found.");
        }
    }
}
