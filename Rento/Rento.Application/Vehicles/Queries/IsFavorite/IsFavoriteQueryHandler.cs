using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Queries.IsFavorite
{
    public class IsFavoriteQueryHandler : IRequestHandler<IsFavoriteQuery, ErrorOr<bool>>
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public IsFavoriteQueryHandler(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<ErrorOr<bool>> Handle(IsFavoriteQuery request, CancellationToken cancellationToken)
        {
            var exists = await _favoriteRepository.ExistsAsync(request.UserId, request.VehicleId);
            return exists;
        }
    }

}
