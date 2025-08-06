using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.RemoveFavoriteVehicle
{
    public record RemoveFavoriteCommand(int UserId, int VehicleId) : IRequest<ErrorOr<Success>>;


}
