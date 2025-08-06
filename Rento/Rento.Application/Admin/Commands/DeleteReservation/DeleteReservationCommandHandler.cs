using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Admin.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler
    : IRequestHandler<DeleteReservationCommand, ErrorOr<Success>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationCommandHandler(
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId);

            if (reservation is null)
                return Error.NotFound("Reservation.NotFound", "Rezervacija nije pronađena.");

            _reservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
