using Microsoft.AspNetCore.Http;

namespace Rento.Contracts.Vehicles
{
    public record UploadVehicleImagesRequest(List<IFormFile> Images);

}
