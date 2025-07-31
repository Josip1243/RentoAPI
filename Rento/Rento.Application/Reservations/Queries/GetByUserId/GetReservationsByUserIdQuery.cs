using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetByUserId
{
    public record GetReservationsByUserIdQuery(int UserId) : IRequest<ErrorOr<List<ReservationResponse>>>;
}
