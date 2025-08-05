using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetAllOwnerVehicles
{
    public class GetAllOwnerVehiclesQueryHandler
    : IRequestHandler<GetAllOwnerVehiclesQuery, ErrorOr<FilteredVehicleResult>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetAllOwnerVehiclesQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<FilteredVehicleResult>> Handle(
            GetAllOwnerVehiclesQuery request,
            CancellationToken cancellationToken)
        {
            return await _vehicleRepository.GetAllForOwnerAsync(request, cancellationToken);
        }
    }
}
