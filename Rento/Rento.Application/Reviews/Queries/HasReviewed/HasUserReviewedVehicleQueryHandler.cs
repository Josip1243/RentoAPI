using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Reviews.Queries.HasReviewed
{
    public class HasUserReviewedVehicleQueryHandler : IRequestHandler<HasUserReviewedVehicleQuery, bool>
    {
        private readonly IReviewRepository _reviewRepository;

        public HasUserReviewedVehicleQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> Handle(HasUserReviewedVehicleQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.HasUserReviewedVehicle(request.ReviewerId, request.VehicleId);
        }
    }

}
