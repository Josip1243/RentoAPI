using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;
using Rento.Domain.Common.Errors;

namespace Rento.Application.Vehicles.Queries.GetVehicleById
{
    public class GetVehicleByIdQueryHandler
    : IRequestHandler<GetVehicleByIdQuery, ErrorOr<VehicleResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<VehicleResponse>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (vehicle is null)
            {
                return Errors.Vehicle.VehicleNotFound;
            }

            var response = _mapper.Map<VehicleResponse>(vehicle);
            return response;
        }
    }
}
