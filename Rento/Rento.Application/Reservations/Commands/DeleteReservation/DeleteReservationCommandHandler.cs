using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler
    : IRequestHandler<DeleteReservationCommand, ErrorOr<Deleted>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (reservation is null)
            {
                return Error.NotFound("Reservation.NotFound", "Rezervacija nije pronađena.");
            }

            _reservationRepository.Remove(reservation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Deleted;
        }
    }
}
