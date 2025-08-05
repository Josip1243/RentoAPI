using ErrorOr;
using MediatR;

namespace Rento.Application.Reservations.Commands.ConfirmReservationPickup
{
    public record ConfirmReservationPickupCommand(int ReservationId, int CurrentUserId) : IRequest<ErrorOr<Success>>;
}
