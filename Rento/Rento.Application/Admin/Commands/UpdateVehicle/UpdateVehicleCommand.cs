using ErrorOr;
using MediatR;

namespace Rento.Application.Admin.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(
    int Id,
    string Brand,
    string Model,
    int Year,
    string RegistrationNumber,
    string ChassisNumber,
    decimal Price
) : IRequest<ErrorOr<Success>>;

}
