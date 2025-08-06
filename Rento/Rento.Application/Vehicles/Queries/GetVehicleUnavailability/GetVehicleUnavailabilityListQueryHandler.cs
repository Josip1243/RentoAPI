using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetVehicleUnavailability
{
    public class GetVehicleUnavailabilityListQueryHandler
    : IRequestHandler<GetVehicleUnavailabilityListQuery, ErrorOr<List<VehicleUnavailabilityDto>>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleUnavailabilityListQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<List<VehicleUnavailabilityDto>>> Handle(
            GetVehicleUnavailabilityListQuery request,
            CancellationToken cancellationToken)
        {
            var fromDate = DateTime.UtcNow.Date;

            var list = await _vehicleRepository.GetUnavailabilityListAsync(request.VehicleId, fromDate);

            return list;
        }
    }
}
