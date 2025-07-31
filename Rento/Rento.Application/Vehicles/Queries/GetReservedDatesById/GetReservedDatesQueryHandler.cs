using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;
using Rento.Domain.Enums;

namespace Rento.Application.Vehicles.Queries.GetReservedDatesById
{
    public class GetReservedDatesQueryHandler : IRequestHandler<GetReservedDatesQuery, ErrorOr<List<ReservationDateRangeResponse>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservedDatesQueryHandler(
            IReservationRepository reservationRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ReservationDateRangeResponse>>> Handle(GetReservedDatesQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository
                .GetReservationsByVehicleIdAsync(request.VehicleId, cancellationToken);

            if (reservations is null || reservations.Count == 0)
            {
                return new List<ReservationDateRangeResponse>();
            }

            var confirmedDates = reservations
                .Select(r => _mapper.Map<ReservationDateRangeResponse>(r))
                .ToList();

            return confirmedDates;
        }
    }
}
