using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Application.Vehicles.Commands.UploadVehicleImage
{
    public class UploadVehicleImagesCommandHandler
    : IRequestHandler<UploadVehicleImagesCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorage; // servis za spremanje slika

        public UploadVehicleImagesCommandHandler(IUnitOfWork unitOfWork, IImageStorageService imageStorage)
        {
            _unitOfWork = unitOfWork;
            _imageStorage = imageStorage;
        }

        public async Task<ErrorOr<Success>> Handle(
            UploadVehicleImagesCommand request,
            CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles
                .GetByIdWithImagesAsync(request.VehicleId, cancellationToken);

            if (vehicle is null)
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");

            if (vehicle.OwnerId != request.OwnerId)
                return Error.Forbidden("Vehicle.Forbidden", "Nemate pristup ovom vozilu.");

            int startingOrder = vehicle.Images.Any()
                ? vehicle.Images.Max(i => i.Order) + 1
                : 0;

            foreach (var (file, index) in request.Images.Select((f, i) => (f, i)))
            {
                var imageUrl = await _imageStorage.SaveImageAsync(file, cancellationToken);

                vehicle.Images.Add(new VehicleImage
                {
                    Url = imageUrl,
                    Name = file.FileName,
                    Order = startingOrder + index
                });
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
    }

}
