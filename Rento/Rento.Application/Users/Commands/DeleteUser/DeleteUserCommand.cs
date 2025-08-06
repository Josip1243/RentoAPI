using ErrorOr;
using MediatR;

namespace Rento.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(int UserId) : IRequest<ErrorOr<Success>>;
}
