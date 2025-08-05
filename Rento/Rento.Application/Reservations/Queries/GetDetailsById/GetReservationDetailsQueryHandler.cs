using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetDetailsById
{
    public class GetReservationDetailsQueryHandler
    : IRequestHandler<GetReservationDetailsQuery, ErrorOr<ReservationDetailsResult>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationDetailsQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<ErrorOr<ReservationDetailsResult>> Handle(
            GetReservationDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _reservationRepository
            .GetReservationDetailsByIdAsync(request.ReservationId, cancellationToken);

            return result is null
                ? Error.NotFound("Reservation.NotFound", "Reservation not found.")
                : result;
        }
    }

}
