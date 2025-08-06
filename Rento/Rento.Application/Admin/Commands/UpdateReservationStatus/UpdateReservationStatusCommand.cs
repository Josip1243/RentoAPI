using ErrorOr;
using MediatR;

namespace Rento.Application.Admin.Commands.UpdateReservationStatus
{
    public record UpdateReservationStatusCommand(int ReservationId, string Status)
    : IRequest<ErrorOr<Success>>;

}
