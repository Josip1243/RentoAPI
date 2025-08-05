using ErrorOr;
using MediatR;

namespace Rento.Application.Reservations.Commands.ConfirmReservationComplete
{
    public record ConfirmReservationCompleteCommand(int ReservationId, int CurrentUserId) : IRequest<ErrorOr<Success>>;
}
