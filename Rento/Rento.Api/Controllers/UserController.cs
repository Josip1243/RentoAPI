using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Users.Commands.BecomeOwner;
using Rento.Application.Users.Commands.DeleteUser;
using Rento.Application.Users.Commands.UpdateUser;
using Rento.Application.Users.Queries.GetAllUsers;
using Rento.Contracts.Users;
using System.Security.Claims;

namespace Rento.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public UserController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut("become-owner")]
        public async Task<IActionResult> BecomeOwner([FromBody] BecomeOwnerRequest request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new BecomeOwnerCommand(
                UserId: userId,
                Oib: request.Oib,
                Address: request.Address,
                PhoneNumber: request.PhoneNumber
            );

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors));
        }

        
    }
}
