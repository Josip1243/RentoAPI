using ErrorOr;
using MediatR;
using Rento.Application.Authentication.Common;

namespace Rento.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
