using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reviews.Common;
using Rento.Domain.Common.Errors;
using Rento.Domain.Entities;
using Rento.Domain.Enums;

namespace Rento.Application.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ErrorOr<ReviewResult>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(
            IReservationRepository reservationRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ReviewResult>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.ReservationId, cancellationToken);

            if (reservation is null)
                return Errors.Review.ReviewNotFound;

            if (reservation.Status != ReservationStatus.Completed)
                return Errors.Review.ReservationNotCompleted;

            if (reservation.UserId != request.ReviewerId)
                return Errors.Review.NotOwner;

            var existingReview = await _reviewRepository.GetByReservationIdAsync(request.ReservationId, cancellationToken);
            if (existingReview is not null)
                return Errors.Review.ReviewAlreadyExists;

            var review = new Review
            {
                ReservationId = reservation.Id,
                ReviewerId = request.ReviewerId,
                VehicleId = reservation.VehicleId,
                OwnerId = reservation.Vehicle.OwnerId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _reviewRepository.AddAsync(review, cancellationToken);

            return _mapper.Map<ReviewResult>(review);
        }
    }
}
