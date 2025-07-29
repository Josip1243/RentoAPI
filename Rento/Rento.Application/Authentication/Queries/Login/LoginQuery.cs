using ErrorOr;
using MediatR;
using Rento.Application.Authentication.Common;

namespace Rento.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
