using ErrorOr;
using MediatR;

namespace Rento.Application.Admin.Commands.DeleteReview
{
    public record DeleteReviewCommand(int ReviewId) : IRequest<ErrorOr<Success>>;

}
