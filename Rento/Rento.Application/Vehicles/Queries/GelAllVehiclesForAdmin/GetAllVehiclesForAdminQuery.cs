using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GelAllVehiclesForAdmin
{
    public record GetAllVehiclesForAdminQuery() : IRequest<List<AdminVehicleResponse>>;

}
