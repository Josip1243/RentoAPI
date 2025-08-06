using Rento.Application.Vehicles.Common;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Application.Vehicles.Queries.GetFavoriteVehicles;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{

    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
        void Remove(Vehicle vehicle);
        Task<bool> ExistsWithRegistrationAsync(string registrationNumber, int? excludeVehicleId = null);
        Task<bool> ExistsWithChassisAsync(string chassisNumber);
        Task<bool> ExistsWithChassisAsync(string chassisNumber, int? excludeVehicleId = null);
        Task<List<Vehicle>> GetFilteredAsync(GetAllVehiclesFilterQuery query, CancellationToken cancellationToken);
        Task<int> GetCount();
        Task<FilteredVehicleResult> GetAllForOwnerAsync(GetAllOwnerVehiclesQuery query, CancellationToken cancellationToken = default);
        Task<Vehicle?> GetByIdWithImagesAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetAllWithOwnerAsync();
        Task<List<VehicleUnavailability>> GetUnavailabilityAsync(int vehicleId);
        Task<List<BusyDateRangeDto>> GetUnavailableDateRangesAsync(int vehicleId, DateTime fromDate);
        void AddUnavailability(VehicleUnavailability entity);
        Task<List<VehicleUnavailabilityDto>> GetUnavailabilityListAsync(int vehicleId, DateTime fromDate);
        Task<VehicleUnavailability?> GetUnavailabilityByIdAsync(int unavailabilityId);
        void RemoveUnavailability(VehicleUnavailability entity);
        Task<List<Vehicle>> GetFilteredFavoritesAsync(GetFavoriteVehiclesQuery request, List<int> favoriteVehicleIds, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int vehicleId);
    }
}
