using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetAllVehiclesFilter
{
    public class GetAllVehiclesFilterQueryHandler
    : IRequestHandler<GetAllVehiclesFilterQuery, ErrorOr<List<VehicleResponse>>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetAllVehiclesFilterQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<VehicleResponse>>> Handle(GetAllVehiclesFilterQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetFilteredAsync(request, cancellationToken);

            return _mapper.Map<List<VehicleResponse>>(vehicles);
        }
    }
}
