using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.DeleteVehicleUnavailability
{
    public record DeleteVehicleUnavailabilityCommand(
    int VehicleId,
    int UnavailabilityId
) : IRequest<ErrorOr<Success>>;
}
