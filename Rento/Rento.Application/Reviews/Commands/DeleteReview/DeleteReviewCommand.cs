using ErrorOr;
using MediatR;

namespace Rento.Application.Reviews.Commands.DeleteReview
{
    public record DeleteReviewCommand(int ReviewId, int UserId) : IRequest<ErrorOr<Deleted>>;

}
