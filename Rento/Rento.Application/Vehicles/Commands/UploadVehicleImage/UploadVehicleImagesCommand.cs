using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Rento.Application.Vehicles.Commands.UploadVehicleImage
{
    public record UploadVehicleImagesCommand(
        int VehicleId,
        int OwnerId,
        List<IFormFile> Images
    ) : IRequest<ErrorOr<Success>>;

}
