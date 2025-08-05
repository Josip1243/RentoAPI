using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetForOwner
{
    public class GetReservationsForOwnerQueryHandler
    : IRequestHandler<GetReservationsForOwnerQuery, ErrorOr<List<OwnerReservationResult>>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationsForOwnerQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ErrorOr<List<OwnerReservationResult>>> Handle(
            GetReservationsForOwnerQuery request,
            CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository
            .GetReservationsForOwnerAsync(request.OwnerId, cancellationToken);

            return reservations;
        }
    }
}
