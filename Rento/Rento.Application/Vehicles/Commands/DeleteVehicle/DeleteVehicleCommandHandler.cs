using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Common.Errors;

namespace Rento.Application.Vehicles.Commands.DeleteVehicle
{
    public class DeleteVehicleCommandHandler
    : IRequestHandler<DeleteVehicleCommand, ErrorOr<Deleted>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRepository _reservationRepository;

        public DeleteVehicleCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
        {
            _unitOfWork = unitOfWork;
            _reservationRepository = reservationRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var hasReservations = await _reservationRepository.AnyForVehicleAsync(request.Id, cancellationToken);
            if (hasReservations)
            {
                return Error.Conflict("Vehicle.HasReservations", "Vehicle has active reservations.");
            }

            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.Id, cancellationToken);

            if (vehicle is null)
            {
                return Errors.Vehicle.VehicleNotFound;
            }

            _unitOfWork.Vehicles.Remove(vehicle);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}
