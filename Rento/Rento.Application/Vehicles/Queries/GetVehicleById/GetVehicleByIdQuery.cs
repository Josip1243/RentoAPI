using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetVehicleById
{
    public record GetVehicleByIdQuery(int Id) : IRequest<ErrorOr<VehicleResult>>;
}
