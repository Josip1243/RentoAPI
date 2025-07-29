using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;
using Rento.Domain.Common.Errors;

namespace Rento.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler
    : IRequestHandler<UpdateVehicleCommand, ErrorOr<VehicleResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateVehicleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<VehicleResponse>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.Id, cancellationToken);

            if (vehicle is null)
            {
                return Errors.Vehicle.VehicleNotFound;
            }

            vehicle.Brand = request.Brand;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.RegistrationNumber = request.RegistrationNumber;
            vehicle.ChassisNumber = request.ChassisNumber;
            vehicle.FuelType = request.FuelType;
            vehicle.DoorsNumber = request.DoorsNumber;
            vehicle.SeatsNumber = request.SeatsNumber;
            vehicle.Price = request.Price;
            vehicle.OwnerId = request.OwnerId;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<VehicleResponse>(vehicle);
        }
    }
}
