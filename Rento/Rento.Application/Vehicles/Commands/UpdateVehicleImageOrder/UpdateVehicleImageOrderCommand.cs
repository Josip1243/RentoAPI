using ErrorOr;
using MediatR;

namespace Rento.Application.Vehicles.Commands.UpdateVehicleImageOrder
{
    public record UpdateVehicleImageOrderCommand(
        int VehicleId,
        int UserId,
        List<ImageOrderDto> Images
    ) : IRequest<ErrorOr<Success>>;

    public record ImageOrderDto(int ImageId, int Order);

}
