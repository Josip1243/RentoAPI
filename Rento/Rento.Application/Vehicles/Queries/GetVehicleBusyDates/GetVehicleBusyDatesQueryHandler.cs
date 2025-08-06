using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetVehicleBusyDates
{
    public class GetVehicleBusyDatesQueryHandler
    : IRequestHandler<GetVehicleBusyDatesQuery, ErrorOr<List<BusyDateRangeDto>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleBusyDatesQueryHandler(
            IReservationRepository reservationRepository,
            IVehicleRepository vehicleRepository)
        {
            _reservationRepository = reservationRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<List<BusyDateRangeDto>>> Handle(
            GetVehicleBusyDatesQuery request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow.Date;

            var reservationRanges = await _reservationRepository
                .GetBusyDateRangesForVehicleAsync(request.VehicleId, now);

            var unavailabilityRanges = await _vehicleRepository
                .GetUnavailableDateRangesAsync(request.VehicleId, now);

            var result = reservationRanges
                .Concat(unavailabilityRanges)
                .OrderBy(x => x.StartDate)
                .ToList();

            return result;
        }
    }
}
