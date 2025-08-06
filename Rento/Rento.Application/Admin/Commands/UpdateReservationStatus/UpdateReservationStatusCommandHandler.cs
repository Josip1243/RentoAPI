using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Enums;

namespace Rento.Application.Admin.Commands.UpdateReservationStatus
{
    public class UpdateReservationStatusCommandHandler
    : IRequestHandler<UpdateReservationStatusCommand, ErrorOr<Success>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateReservationStatusCommandHandler(
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation is null)
                return Error.NotFound("Reservation.NotFound", "Rezervacija nije pronađena.");

            if (!Enum.TryParse<ReservationStatus>(request.Status, true, out var newStatus))
                return Error.Validation("Reservation.InvalidStatus", "Neispravan status rezervacije.");

            reservation.Status = newStatus;
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
