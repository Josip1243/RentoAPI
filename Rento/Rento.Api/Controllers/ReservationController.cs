using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Reservations.Commands.ConfirmReservationComplete;
using Rento.Application.Reservations.Commands.ConfirmReservationPickup;
using Rento.Application.Reservations.Commands.CreateReservation;
using Rento.Application.Reservations.Commands.DeleteReservation;
using Rento.Application.Reservations.Queries.GetById;
using Rento.Application.Reservations.Queries.GetByUserId;
using Rento.Application.Reservations.Queries.GetDetailsById;
using Rento.Application.Reservations.Queries.GetForOwner;
using Rento.Contracts.Reservations;
using System.Security.Claims;

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

        [HttpGet("user")]
        public async Task<IActionResult> GetByUserId(CancellationToken cancellationToken)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var query = new GetReservationsByUserIdQuery(userId);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                reservations => Ok(reservations),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request, CancellationToken cancellationToken)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new CreateReservationCommand(
                userId,
                request.VehicleId,
                request.StartDate,
                request.EndDate
            );

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                reservation => Ok(reservation),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteReservationCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var query = new GetReservationDetailsQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                details => Ok(details),
                errors => Problem(errors)
            );
        }

        [HttpGet("for-owner")]
        public async Task<IActionResult> GetForOwner()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var query = new GetReservationsForOwnerQuery(userId);
            var result = await _mediator.Send(query);

            return result.Match(
                reservations => Ok(reservations),
                errors => Problem(errors)
            );
        }

        [HttpPatch("{id:int}/confirm-pickup")]
        public async Task<IActionResult> ConfirmPickup(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new ConfirmReservationPickupCommand(id, userId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpPatch("{id:int}/confirm-return")]
        public async Task<IActionResult> ConfirmReturn(int id)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new ConfirmReservationCompleteCommand(id, userId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

    }
}
