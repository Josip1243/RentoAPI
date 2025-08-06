using ErrorOr;
using MediatR;

namespace Rento.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
    int Id,
    string FirstName,
    string LastName,
    string? Oib,
    string? Address,
    string? PhoneNumber,
    string? DriversLicenceNumber,
    string Role
) : IRequest<ErrorOr<Success>>;

}
