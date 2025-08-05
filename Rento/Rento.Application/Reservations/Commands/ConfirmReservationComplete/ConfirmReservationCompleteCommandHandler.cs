using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Enums;

namespace Rento.Application.Reservations.Commands.ConfirmReservationComplete
{
    public class ConfirmReturnCommandHandler
    : IRequestHandler<ConfirmReservationCompleteCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmReturnCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(ConfirmReservationCompleteCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _unitOfWork.Reservations
                .GetReservationWithVehicleAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
            {
                return Error.NotFound("Reservation.NotFound", "Reservation not found.");
            }

            if (reservation.Vehicle.OwnerId != request.CurrentUserId)
            {
                return Error.Forbidden("Reservation.Forbidden", "You cannot edit this reservation.");
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow.Date);
            var endDate = DateOnly.FromDateTime(reservation.EndDate.Date);

            if (today != endDate)
            {
                return Error.Validation("Reservation.InvalidDate", "Reservation can be ended on the last day.");
            }

            reservation.Status = ReservationStatus.Completed;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
