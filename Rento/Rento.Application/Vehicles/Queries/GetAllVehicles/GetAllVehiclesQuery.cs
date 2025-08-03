using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetAllVehicles
{
    public record GetAllVehiclesQuery() : IRequest<ErrorOr<List<VehicleResult>>>;
}
