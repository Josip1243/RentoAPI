using ErrorOr;
using MediatR;

namespace Rento.Application.Users.Commands.BecomeOwner
{
    public record BecomeOwnerCommand(
    int UserId,
    string Oib,
    string Address,
    string PhoneNumber
) : IRequest<ErrorOr<Success>>;

}
