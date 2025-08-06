using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Enums;

namespace Rento.Application.Reservations.Commands.ConfirmReservationPickup
{
    public class ConfirmPickupCommandHandler
    : IRequestHandler<ConfirmReservationPickupCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmPickupCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(
            ConfirmReservationPickupCommand request,
            CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.Reservations
                .GetReservationWithVehicleAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
            {
                return Error.NotFound("Reservation.NotFound", "Reservation not found.");
            }

            //if (reservation.Vehicle.OwnerId != request.CurrentUserId)
            //{
            //    return Error.Forbidden("Reservation.Forbidden", "You cannot edit this reservation.");
            //}

            reservation.Status = ReservationStatus.Active;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
