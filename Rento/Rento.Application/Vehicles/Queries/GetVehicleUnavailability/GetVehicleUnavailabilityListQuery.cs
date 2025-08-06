using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetVehicleUnavailability
{
    public record GetVehicleUnavailabilityListQuery(int VehicleId)
     : IRequest<ErrorOr<List<VehicleUnavailabilityDto>>>;
}
