using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.DeleteVehicleUnavailability
{
    public class DeleteVehicleUnavailabilityCommandHandler
    : IRequestHandler<DeleteVehicleUnavailabilityCommand, ErrorOr<Success>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVehicleUnavailabilityCommandHandler(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteVehicleUnavailabilityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _vehicleRepository.GetUnavailabilityByIdAsync(request.UnavailabilityId);

            if (entity is null || entity.VehicleId != request.VehicleId)
            {
                return Error.NotFound("Unavailability.NotFound", "Unos nedostupnosti nije pronađen.");
            }

            _vehicleRepository.RemoveUnavailability(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
