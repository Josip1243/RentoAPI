using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ErrorOr<Success>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReviewCommandHandler(
            IReservationRepository reservationRepository,
            IReviewRepository reviewRepository,
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<Success>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            // Nađi završenu rezervaciju za tog korisnika i to vozilo
            var reservation = await _reservationRepository.HasCompletedReservation(request.ReviewerId, request.VehicleId);

            if (reservation is false)
            {
                return Error.Validation("Review.NotAllowed", "Nemate završenu rezervaciju za ovo vozilo.");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);

            if (vehicle is null)
            {
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");
            }

            var review = new Review
            {
                ReviewerId = request.ReviewerId,
                VehicleId = request.VehicleId,
                OwnerId = vehicle.OwnerId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }


}
