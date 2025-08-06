using Mapster;
using Rento.Application.Reviews.Commands.CreateReview;
using Rento.Application.Reviews.Common;
using Rento.Contracts.Reviews;
using Rento.Domain.Entities;

namespace Rento.Api.Mapping
{
    public class ReviewMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Review, ReviewResponse>();
            config.NewConfig<ReviewResult, ReviewResponse>();

            config.NewConfig<CreateReviewRequest, CreateReviewCommand>();
        }
    }
}
