using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Vehicles.Commands.AddFavoriteVehicle;
using Rento.Application.Vehicles.Commands.AddVehicleUnavailability;
using Rento.Application.Vehicles.Commands.CreateVehicle;
using Rento.Application.Vehicles.Commands.DeleteVehicle;
using Rento.Application.Vehicles.Commands.DeleteVehicleImage;
using Rento.Application.Vehicles.Commands.DeleteVehicleUnavailability;
using Rento.Application.Vehicles.Commands.RemoveFavoriteVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicleImageOrder;
using Rento.Application.Vehicles.Commands.UploadVehicleImage;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Application.Vehicles.Queries.GetFavoriteVehicles;
using Rento.Application.Vehicles.Queries.GetReservedDatesById;
using Rento.Application.Vehicles.Queries.GetVehicleBusyDates;
using Rento.Application.Vehicles.Queries.GetVehicleById;
using Rento.Application.Vehicles.Queries.GetVehicleUnavailability;
using Rento.Application.Vehicles.Queries.IsFavorite;
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
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = _mapper.Map<CreateVehicleCommand>(request) with { OwnerId = userId };

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

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = _mapper.Map<UpdateVehicleCommand>(request) with { OwnerId = userId };

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

        [HttpPatch("{vehicleId:int}/images/order")]
        public async Task<IActionResult> UpdateImageOrder(int vehicleId, [FromBody] UpdateVehicleImageOrderRequest request)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new UpdateVehicleImageOrderCommand(
                vehicleId,
                userId,
                _mapper.Map<List<Application.Vehicles.Commands.UpdateVehicleImageOrder.ImageOrderDto>>(request.Images)
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpPost("{vehicleId:int}/images")]
        public async Task<IActionResult> UploadImages(
            int vehicleId,
            [FromForm] List<IFormFile> images)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new UploadVehicleImagesCommand(vehicleId, userId, images);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{vehicleId:int}/images/{imageId:int}")]
        public async Task<IActionResult> DeleteImage(int vehicleId, int imageId)
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var command = new DeleteVehicleImageCommand(vehicleId, imageId, userId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        // Unavailabilities
        [HttpGet("{vehicleId:int}/busy-dates")]
        public async Task<IActionResult> GetBusyDates(int vehicleId)
        {
            var query = new GetVehicleBusyDatesQuery(vehicleId);
            var result = await _mediator.Send(query);

            return result.Match(
                dates => Ok(dates),
                errors => Problem(errors)
            );
        }

        [HttpGet("{vehicleId:int}/unavailability")]
        public async Task<IActionResult> GetUnavailabilityList(int vehicleId)
        {
            var query = new GetVehicleUnavailabilityListQuery(vehicleId);
            var result = await _mediator.Send(query);

            return result.Match(
                list => Ok(list),
                errors => Problem(errors)
            );
        }

        [HttpPost("{vehicleId:int}/unavailability")]
        public async Task<IActionResult> AddUnavailability(
            int vehicleId,
            [FromBody] AddVehicleUnavailabilityRequest request)
        {
            var command = new AddVehicleUnavailabilityCommand(
                vehicleId,
                request.StartDate,
                request.EndDate,
                request.Reason
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{vehicleId:int}/unavailability/{unavailabilityId:int}")]
        public async Task<IActionResult> DeleteUnavailability(int vehicleId, int unavailabilityId)
        {
            var command = new DeleteVehicleUnavailabilityCommand(vehicleId, unavailabilityId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> GetFavorites([FromQuery] VehicleFilterRequest filter, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query = _mapper.Map<GetFavoriteVehiclesQuery>(filter) with { UserId = userId };

            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                vehicles => Ok(_mapper.Map<VehicleListResponse>(vehicles)),
                errors => Problem(errors)
            );
        }

        [HttpPost("{vehicleId:int}/favorite")]
        public async Task<IActionResult> AddToFavorites(int vehicleId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var command = new AddFavoriteCommand(userId, vehicleId);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors));
        }

        [HttpDelete("{vehicleId:int}/favorite")]
        public async Task<IActionResult> RemoveFromFavorites(int vehicleId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var command = new RemoveFavoriteCommand(userId, vehicleId);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("{vehicleId:int}/favorite")]
        public async Task<IActionResult> IsFavorite(int vehicleId, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query = new IsFavoriteQuery(userId, vehicleId);
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                isFavorite => Ok(isFavorite),
                errors => Problem(errors)
            );
        }
    }
}
