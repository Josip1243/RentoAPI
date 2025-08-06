using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.RemoveFavoriteVehicle
{
    public class RemoveFavoriteCommandHandler : IRequestHandler<RemoveFavoriteCommand, ErrorOr<Success>>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveFavoriteCommandHandler(IFavoriteRepository favoriteRepository, IUnitOfWork unitOfWork)
        {
            _favoriteRepository = favoriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
        {
            var favorite = await _favoriteRepository.GetAsync(request.UserId, request.VehicleId);

            if (favorite is null)
            {
                return Error.NotFound("Favorite.NotFound", "Vozilo nije u favoritima.");
            }

            _favoriteRepository.Remove(favorite);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }

}
