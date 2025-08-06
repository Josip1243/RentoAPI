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
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReservationCommandHandler(
            IReservationRepository reservationRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IVehicleRepository vehicleRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<ReservationResponse>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle is null)
            {
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");
            }

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

            var days = (request.EndDate.Date - request.StartDate.Date).Days + 1;
            var totalAmount = vehicle.Price * days;


            // Fake payment 

            var payment = new Payment
            {
                ReservationId = reservation.Id,
                Amount = totalAmount,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = PaymentMethod.Card,
                Status = PaymentStatus.Paid
            };
            _reservationRepository.AddPayment(payment);

            var payout = new OwnerPayout
            {
                UserId = vehicle.OwnerId,
                ReservationId = reservation.Id,
                Amount = totalAmount * 0.9m, // npr. 90% ide vlasniku
                PayoutDate = DateTime.UtcNow,
                Status = PayoutStatus.Pending
            };
            _reservationRepository.AddOwnerPayout(payout);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReservationResponse>(reservation);
        }
    }
}
