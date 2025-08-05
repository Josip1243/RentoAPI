using Azure.Core;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Vehicles.Commands.CreateVehicle;
using Rento.Application.Vehicles.Commands.DeleteVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Application.Vehicles.Queries.GetReservedDatesById;
using Rento.Application.Vehicles.Queries.GetVehicleById;
using Rento.Contracts.Reservations;
using Rento.Contracts.Vehicles;
using System.Security.Claims;

namespace Rento.Api.Controllers
{
    [Route("[controller]")]
    public class VehicleController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public VehicleController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllVehiclesQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                vehicles => Ok(_mapper.Map<List<VehicleResponse>>(vehicles)),
                errors => Problem(errors)
            );
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFiltered([FromQuery] VehicleFilterRequest filter, CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetAllVehiclesFilterQuery>(filter);

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                vehicles => Ok(_mapper.Map<VehicleListResponse>(vehicles)),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var query = new GetVehicleByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                vehicle => Ok(_mapper.Map<VehicleResponse>(vehicle)),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateVehicleCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                vehicle => Ok(_mapper.Map<VehicleResponse>(vehicle)),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, UpdateVehicleRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
            {
                return BadRequest("ID u ruti i tijelu zahtjeva moraju biti isti.");
            }

            var command = _mapper.Map<UpdateVehicleCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                vehicle => Ok(_mapper.Map<VehicleResponse>(vehicle)),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteVehicleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}/reserved-dates")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReservedDates(int id, CancellationToken cancellationToken)
        {
            var command = new GetReservedDatesQuery(id);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                dates => Ok(_mapper.Map<List<ReservationDateRangeResponse>>(dates)),
                errors => Problem(errors)
            );
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyVehicles([FromQuery] VehicleFilterRequest request)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var query = _mapper.Map<GetAllOwnerVehiclesQuery>(request) with { OwnerId = userId };

            var result = await _mediator.Send(query);

            return result.Match(
                value => Ok(value),
                errors => Problem(errors)
            );
        }
    }
}
