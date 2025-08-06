using ErrorOr;
using MediatR;

namespace Rento.Application.Users.Commands.BecomeOwner
{
    public record BecomeOwnerCommand(
    int UserId,
    string Oib,
    string Address,
    string PhoneNumber,
    string Iban,
    string AccountHolderName,
    string BankName
) : IRequest<ErrorOr<Success>>;

}
