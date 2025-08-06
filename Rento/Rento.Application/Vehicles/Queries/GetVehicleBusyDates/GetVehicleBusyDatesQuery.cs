using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetVehicleBusyDates
{
    public record GetVehicleBusyDatesQuery(int VehicleId)
    : IRequest<ErrorOr<List<BusyDateRangeDto>>>;
}
