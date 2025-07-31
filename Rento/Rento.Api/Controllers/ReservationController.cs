using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Reservations.Commands.CreateReservation;
using Rento.Application.Reservations.Commands.DeleteReservation;
using Rento.Application.Reservations.Queries.GetById;
using Rento.Application.Reservations.Queries.GetByUserId;
using Rento.Contracts.Reservations;

namespace Rento.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ReservationsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetReservationByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                reservation => Ok(reservation),
                errors => Problem(errors)
            );
        }

        [AllowAnonymous]
        [HttpGet("user/{userId:int}")]
        public async Task<IActionResult> GetByUserId(int userId, CancellationToken cancellationToken)
        {
            var query = new GetReservationsByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                reservations => Ok(reservations),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateReservationCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                reservation => Ok(reservation),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteReservationCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

    }
}
