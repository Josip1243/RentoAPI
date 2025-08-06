using MediatR;
using Rento.Application.Admin.Common;

namespace Rento.Application.Admin.Queries.GetAllReservations
{
    public record GetAllReservationsForAdminQuery() : IRequest<List<AdminReservationResponse>>;

}
