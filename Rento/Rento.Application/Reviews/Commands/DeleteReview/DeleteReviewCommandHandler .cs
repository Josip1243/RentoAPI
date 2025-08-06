using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ErrorOr<Deleted>>
    {
        private readonly IReviewRepository _reviewRepository;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);

            if (review is null)
                return Error.NotFound(description: "Recenzija nije pronađena.");

            //if (review.ReviewerId != request.UserId)
            //    return Error.Forbidden(description: "Niste ovlašteni obrisati ovu recenziju.");

            await _reviewRepository.DeleteAsync(review, cancellationToken);

            return Result.Deleted;
        }
    }
}
