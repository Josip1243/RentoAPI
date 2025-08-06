using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.AddVehicleUnavailability
{
    public record AddVehicleUnavailabilityCommand(
    int VehicleId,
    DateTime StartDate,
    DateTime EndDate,
    string? Reason
) : IRequest<ErrorOr<Success>>;
}
