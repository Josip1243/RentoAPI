using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Queries.IsFavorite
{
    public record IsFavoriteQuery(int UserId, int VehicleId) : IRequest<ErrorOr<bool>>;

}
