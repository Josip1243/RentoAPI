using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.AddFavoriteVehicle
{
    public record AddFavoriteCommand(int UserId, int VehicleId) : IRequest<ErrorOr<Success>>;

}
