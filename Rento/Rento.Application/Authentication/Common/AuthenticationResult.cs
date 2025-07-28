using Rento.Domain.Entities;

namespace Rento.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token
        );
}
