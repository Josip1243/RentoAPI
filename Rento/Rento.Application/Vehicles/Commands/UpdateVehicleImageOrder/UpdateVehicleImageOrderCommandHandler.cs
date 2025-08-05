using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.UpdateVehicleImageOrder
{
    public class UpdateVehicleImageOrderCommandHandler
    : IRequestHandler<UpdateVehicleImageOrderCommand, ErrorOr<Success>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVehicleImageOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateVehicleImageOrderCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _unitOfWork.Vehicles.GetByIdWithImagesAsync(request.VehicleId, cancellationToken);

            if (vehicle is null)
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");

            if (vehicle.OwnerId != request.UserId)
                return Error.Forbidden("Vehicle.Forbidden", "Nemate pristup ovom vozilu.");

            foreach (var image in vehicle.Images)
            {
                var match = request.Images.FirstOrDefault(i => i.ImageId == image.Id);
                if (match is not null)
                    image.Order = match.Order;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
