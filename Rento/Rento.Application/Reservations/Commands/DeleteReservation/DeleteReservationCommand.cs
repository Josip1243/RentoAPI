using ErrorOr;
using MediatR;

namespace Rento.Application.Reservations.Commands.DeleteReservation
{

    public record DeleteReservationCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
