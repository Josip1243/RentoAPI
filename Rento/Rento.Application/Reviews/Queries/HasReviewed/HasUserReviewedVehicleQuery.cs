using MediatR;

namespace Rento.Application.Reviews.Queries.HasReviewed
{
    public record HasUserReviewedVehicleQuery(
    int VehicleId,
    int ReviewerId
) : IRequest<bool>;

}
