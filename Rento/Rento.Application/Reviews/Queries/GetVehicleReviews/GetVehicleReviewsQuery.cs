using ErrorOr;
using MediatR;
using Rento.Application.Reviews.Common;

namespace Rento.Application.Reviews.Queries.GetVehicleReviews
{
    public record GetVehicleReviewsQuery(int VehicleId) : IRequest<ErrorOr<List<ReviewResult>>>;

}
