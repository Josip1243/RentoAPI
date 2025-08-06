using MediatR;
using Rento.Application.Admin.Common;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Admin.Queries.GetAllReviews
{
    public class GetAllReviewsForAdminQueryHandler
    : IRequestHandler<GetAllReviewsForAdminQuery, List<AdminReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAllReviewsForAdminQueryHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<AdminReviewResponse>> Handle(GetAllReviewsForAdminQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetAllWithVehicleAndReviewerAsync();

            return reviews.Select(r => new AdminReviewResponse
            {
                Id = r.Id,
                VehicleId = r.VehicleId,
                VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                ReviewerId = r.ReviewerId,
                ReviewerName = $"{r.Reviewer.FirstName} {r.Reviewer.LastName}",
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }
    }

}
