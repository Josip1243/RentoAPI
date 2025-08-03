using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetAllVehicles
{
    public class GetAllVehiclesQueryHandler
    : IRequestHandler<GetAllVehiclesQuery, ErrorOr<List<VehicleResult>>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<VehicleResult>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync(cancellationToken);

            var response = _mapper.Map<List<VehicleResult>>(vehicles);

            return response;
        }
    }
}
