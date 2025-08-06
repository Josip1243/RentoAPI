using MediatR;

namespace Rento.Application.Reviews.Queries.CanUserReview
{
    public record CanUserReviewQuery(int VehicleId, int UserId) : IRequest<bool>;

}
