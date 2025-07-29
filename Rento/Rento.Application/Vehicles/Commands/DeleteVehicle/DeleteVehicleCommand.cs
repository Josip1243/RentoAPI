using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.DeleteVehicle
{
    public record DeleteVehicleCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}
