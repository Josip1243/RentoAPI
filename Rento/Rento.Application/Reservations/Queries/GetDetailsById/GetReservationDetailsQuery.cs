using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetDetailsById
{
    public record GetReservationDetailsQuery(int ReservationId) : IRequest<ErrorOr<ReservationDetailsResult>>;

}
