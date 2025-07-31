using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetById
{
    public record GetReservationByIdQuery(int Id) : IRequest<ErrorOr<ReservationResponse>>;
}
