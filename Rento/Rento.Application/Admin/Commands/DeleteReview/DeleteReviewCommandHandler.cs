using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Admin.Commands.DeleteReview
{
    public class DeleteReviewCommandHandler
    : IRequestHandler<DeleteReviewCommand, ErrorOr<Success>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteReviewCommandHandler(
            IReviewRepository reviewRepository,
            IUnitOfWork unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetByIdAsync(request.ReviewId);

            if (review is null)
                return Error.NotFound("Review.NotFound", "Recenzija nije pronađena.");

            _reviewRepository.Delete(review);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
