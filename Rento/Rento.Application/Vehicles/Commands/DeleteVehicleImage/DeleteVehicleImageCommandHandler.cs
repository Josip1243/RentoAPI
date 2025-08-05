using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.DeleteVehicleImage
{
    public class DeleteVehicleImageCommandHandler
    : IRequestHandler<DeleteVehicleImageCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorage;

        public DeleteVehicleImageCommandHandler(
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorage)
        {
            _unitOfWork = unitOfWork;
            _imageStorage = imageStorage;
        }

        public async Task<ErrorOr<Success>> Handle(
            DeleteVehicleImageCommand request,
            CancellationToken cancellationToken)
        {
            var image = await _unitOfWork.VehicleImages.GetByIdAsync(request.ImageId, cancellationToken);

            if (image is null || image.VehicleId != request.VehicleId)
                return Error.NotFound("Image.NotFound", "Slika nije pronađena ili ne pripada vozilu.");

            var vehicle = await _unitOfWork.Vehicles.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null || vehicle.OwnerId != request.UserId)
                return Error.Forbidden("Vehicle.Forbidden", "Nemate pravo na brisanje slike.");

            _unitOfWork.VehicleImages.Remove(image);
            await _imageStorage.DeleteImageAsync(image.Url, cancellationToken);

            await _unitOfWork.VehicleImages.FixOrderForVehicleAsync(request.VehicleId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
