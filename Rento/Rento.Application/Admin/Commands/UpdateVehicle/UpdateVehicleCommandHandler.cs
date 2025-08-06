using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Admin.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, ErrorOr<Success>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleCommandHandler(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.Id);

            if (vehicle is null)
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");

            vehicle.Brand = request.Brand;
            vehicle.Model = request.Model;
            vehicle.Year = request.Year;
            vehicle.RegistrationNumber = request.RegistrationNumber;
            vehicle.ChassisNumber = request.ChassisNumber;
            vehicle.Price = request.Price;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
