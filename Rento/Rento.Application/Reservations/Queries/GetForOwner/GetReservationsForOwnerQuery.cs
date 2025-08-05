using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetForOwner
{
    public record GetReservationsForOwnerQuery(int OwnerId) : IRequest<ErrorOr<List<OwnerReservationResult>>>;

}
