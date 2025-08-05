using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;
using Rento.Domain.Entities;
using Rento.Domain.Enums;

namespace Rento.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, ErrorOr<ReservationResponse>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<ReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = new Reservation
            {
                UserId = request.UserId,
                VehicleId = request.VehicleId,
                ReservationDate = DateTime.UtcNow,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = ReservationStatus.Confirmed,
                CreatedAt = DateTime.UtcNow
            };

            await _reservationRepository.AddAsync(reservation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReservationResponse>(reservation);
        }
    }
}
