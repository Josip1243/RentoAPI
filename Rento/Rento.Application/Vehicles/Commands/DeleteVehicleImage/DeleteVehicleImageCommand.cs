using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.DeleteVehicleImage
{
    public record DeleteVehicleImageCommand(
    int VehicleId,
    int ImageId,
    int UserId
) : IRequest<ErrorOr<Success>>;

}
