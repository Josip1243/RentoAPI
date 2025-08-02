using ErrorOr;

namespace Rento.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Review
        {
            public static Error ReviewNotFound => Error.NotFound(
                code: "Review.ReviewNotFound",
                description: "Review is not found.");

            public static Error ReviewAlreadyExists => Error.Conflict(
                code: "Review.ReviewAlreadyExists",
                description: "Review already exists for this reservation.");

            public static Error ReservationNotCompleted => Error.Validation(
                code: "Review.ReservationNotCompleted",
                description: "You can only leave a review for completed reservations.");

            public static Error NotOwner => Error.Forbidden(
                code: "Review.NotOwner",
                description: "You can only leave a review for your own reservations.");
        }
    }
}
