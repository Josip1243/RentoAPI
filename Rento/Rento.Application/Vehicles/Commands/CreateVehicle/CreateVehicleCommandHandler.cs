using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;
using Rento.Domain.Entities;

namespace Rento.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler
    : IRequestHandler<CreateVehicleCommand, ErrorOr<VehicleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<VehicleResponse>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = new Vehicle
            {
                Brand = request.Brand,
                Model = request.Model,
                Year = request.Year,
                RegistrationNumber = request.RegistrationNumber,
                ChassisNumber = request.ChassisNumber,
                FuelType = request.FuelType,
                DoorsNumber = request.DoorsNumber,
                SeatsNumber = request.SeatsNumber,
                Price = request.Price,
                OwnerId = request.OwnerId
            };

            await _unitOfWork.Vehicles.AddAsync(vehicle, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<VehicleResponse>(vehicle);
        }
    }
}
