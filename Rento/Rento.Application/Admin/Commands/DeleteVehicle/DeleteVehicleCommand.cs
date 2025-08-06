using ErrorOr;
using MediatR;

namespace Rento.Application.Admin.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(int VehicleId) : IRequest<ErrorOr<Success>>;

}
