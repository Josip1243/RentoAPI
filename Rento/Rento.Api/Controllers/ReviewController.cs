using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Reviews.Commands.CreateReview;
using Rento.Application.Reviews.Commands.DeleteReview;
using Rento.Application.Reviews.Queries.GetVehicleReviews;
using Rento.Contracts.Reviews;
using System.Security.Claims;

namespace Rento.Api.Controllers
{
    [Route("[controller]")]
    public class ReviewController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ReviewController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewRequest request, CancellationToken cancellationToken)
        {
            var reviewerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = _mapper.Map<CreateReviewCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                review => Ok(_mapper.Map<ReviewResponse>(review)),
                errors => Problem(errors));
        }

        [AllowAnonymous]
        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviewsForVehicle(int id)
        {
            var result = await _mediator.Send(new GetVehicleReviewsQuery(id));

            return result.Match(
                reviews => Ok(_mapper.Map<List<ReviewResponse>>(reviews)),
                errors => Problem(errors));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new DeleteReviewCommand(id, userId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

    }
}
