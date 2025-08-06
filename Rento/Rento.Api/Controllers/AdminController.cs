using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Admin.Commands.DeleteReservation;
using Rento.Application.Admin.Commands.DeleteReview;
using Rento.Application.Admin.Commands.DeleteVehicle;
using Rento.Application.Admin.Commands.UpdateReservationStatus;
using Rento.Application.Admin.Commands.UpdateVehicle;
using Rento.Application.Admin.Queries.GetAllReservations;
using Rento.Application.Admin.Queries.GetAllReviews;
using Rento.Application.Users.Commands.DeleteUser;
using Rento.Application.Users.Commands.UpdateUser;
using Rento.Application.Users.Queries.GetAllUsers;
using Rento.Application.Vehicles.Queries.GelAllVehiclesForAdmin;
using Rento.Contracts.Admin;
using Rento.Contracts.Users;

namespace Rento.Api.Controllers
{
    [Route("[controller]")]
    public class AdminController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AdminController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpDelete("users/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpPut("users/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
        {
            var command = new UpdateUserCommand(
                Id: id,
                FirstName: request.FirstName,
                LastName: request.LastName,
                Oib: request.Oib,
                Address: request.Address,
                PhoneNumber: request.PhoneNumber,
                DriversLicenceNumber: request.DriversLicenceNumber,
                Role: request.Role
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        

        // Reservations

        [HttpGet("reservations")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReservationsForAdmin()
        {
            var result = await _mediator.Send(new GetAllReservationsForAdminQuery());
            return Ok(result);
        }

        [HttpPatch("reservations/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateReservationStatus(int id, [FromBody] UpdateReservationStatusRequest request)
        {
            var command = new UpdateReservationStatusCommand(id, request.Status);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("reservations/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var result = await _mediator.Send(new DeleteReservationCommand(id));

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        // Reviews

        [HttpGet("reviews")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllReviewsForAdmin()
        {
            var result = await _mediator.Send(new GetAllReviewsForAdminQuery());
            return Ok(result);
        }

        [HttpDelete("reviews/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _mediator.Send(new DeleteReviewCommand(id));

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        // Vehicles

        [HttpGet("vehicles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVehiclesForAdmin()
        {
            var result = await _mediator.Send(new GetAllVehiclesForAdminQuery());
            return Ok(result);
        }

        [HttpPut("vehicles/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleAdminRequest request)
        {
            var command = new UpdateVehicleCommand(
                id,
                request.Brand,
                request.Model,
                request.Year,
                request.RegistrationNumber,
                request.ChassisNumber,
                request.Price
            );

            var result = await _mediator.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("vehicles/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _mediator.Send(new DeleteVehicleCommand(id));

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
