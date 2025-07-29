using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Commands.UpdateVehicle
{
    public record UpdateVehicleCommand(
        int Id,
        string Brand,
        string Model,
        int Year,
        string RegistrationNumber,
        string ChassisNumber,
        string FuelType,
        int DoorsNumber,
        int SeatsNumber,
        decimal Price,
        int OwnerId
    ) : IRequest<ErrorOr<VehicleResponse>>;
}
