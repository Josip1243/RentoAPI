using ErrorOr;
using MediatR;

namespace Rento.Application.Admin.Commands.DeleteReservation
{
    public record DeleteReservationCommand(int ReservationId) : IRequest<ErrorOr<Success>>;

}
