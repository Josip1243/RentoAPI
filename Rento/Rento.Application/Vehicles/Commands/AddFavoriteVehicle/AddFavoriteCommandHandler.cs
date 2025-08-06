using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Application.Vehicles.Commands.AddFavoriteVehicle
{
    public class AddFavoriteCommandHandler : IRequestHandler<AddFavoriteCommand, ErrorOr<Success>>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddFavoriteCommandHandler(
            IFavoriteRepository favoriteRepository,
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _favoriteRepository = favoriteRepository;
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
        {
            var vehicleExists = await _vehicleRepository.ExistsAsync(request.VehicleId);
            if (!vehicleExists)
            {
                return Error.NotFound("Vehicle.NotFound", "Vozilo nije pronađeno.");
            }

            var alreadyExists = await _favoriteRepository.ExistsAsync(request.UserId, request.VehicleId);
            if (alreadyExists)
            {
                return Error.Conflict("Favorite.Exists", "Vozilo je već u favoritima.");
            }

            var favorite = new Favorite
            {
                UserId = request.UserId,
                VehicleId = request.VehicleId,
                CreatedAt = DateTime.UtcNow
            };

            _favoriteRepository.Add(favorite);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
