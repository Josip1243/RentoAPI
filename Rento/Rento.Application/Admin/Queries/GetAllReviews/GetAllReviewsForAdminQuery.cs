using MediatR;
using Rento.Application.Admin.Common;

namespace Rento.Application.Admin.Queries.GetAllReviews
{
    public record GetAllReviewsForAdminQuery() : IRequest<List<AdminReviewResponse>>;

}
