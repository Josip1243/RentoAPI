using ErrorOr;
using MediatR;
using Rento.Application.Reviews.Common;

namespace Rento.Application.Reviews.Commands.CreateReview
{
    public record CreateReviewCommand(
    int VehicleId,
    int Rating,
    string? Comment,
    int ReviewerId 
) : IRequest<ErrorOr<Success>>;
}
