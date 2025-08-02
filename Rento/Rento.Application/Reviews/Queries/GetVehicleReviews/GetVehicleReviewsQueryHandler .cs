using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reviews.Common;

namespace Rento.Application.Reviews.Queries.GetVehicleReviews
{
    public class GetVehicleReviewsQueryHandler : IRequestHandler<GetVehicleReviewsQuery, ErrorOr<List<ReviewResult>>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetVehicleReviewsQueryHandler(
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ReviewResult>>> Handle(GetVehicleReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetByVehicleIdAsync(request.VehicleId, cancellationToken);

            var result = reviews
                .Select(r => _mapper.Map<ReviewResult>(r))
                .ToList();

            return result;
        }
    }
}
