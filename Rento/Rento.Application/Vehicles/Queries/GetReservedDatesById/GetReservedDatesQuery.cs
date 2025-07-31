using ErrorOr;
using MediatR;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Vehicles.Queries.GetReservedDatesById
{
    public record GetReservedDatesQuery(int VehicleId) : IRequest<ErrorOr<List<ReservationDateRangeResponse>>>;

}
