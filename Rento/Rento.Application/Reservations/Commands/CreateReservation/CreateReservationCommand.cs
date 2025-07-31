using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Commands.CreateReservation
{
    public record CreateReservationCommand(
    int UserId,
    int VehicleId,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<ErrorOr<ReservationResponse>>;
}
