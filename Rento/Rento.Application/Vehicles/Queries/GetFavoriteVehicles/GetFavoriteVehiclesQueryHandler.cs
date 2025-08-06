using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetFavoriteVehicles
{
    public class GetFavoriteVehiclesQueryHandler
    : IRequestHandler<GetFavoriteVehiclesQuery, ErrorOr<FilteredVehicleResult>>
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetFavoriteVehiclesQueryHandler(IFavoriteRepository favoriteRepository, IMapper mapper, IVehicleRepository vehicleRepository)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ErrorOr<FilteredVehicleResult>> Handle(GetFavoriteVehiclesQuery request, CancellationToken cancellationToken)
        {
            var favorites = await _favoriteRepository.GetFavoritesByUserIdAsync(request.UserId);

            var favoriteVehicleIds = favorites.Select(f => f.VehicleId).ToList();

            var filteredVehicles = await _vehicleRepository
                .GetFilteredFavoritesAsync(request, favoriteVehicleIds, cancellationToken);

            var mapped = _mapper.Map<List<VehicleResult>>(filteredVehicles);

            return new FilteredVehicleResult(
                Vehicles: mapped,
                TotalCount: favoriteVehicleIds.Count
            );
        }
    }

}
