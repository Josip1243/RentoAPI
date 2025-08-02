using Mapster;
using Rento.Application.Reviews.Common;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Mapping
{
    public class ReviewMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Review, ReviewResult>();
        }
    }
}
