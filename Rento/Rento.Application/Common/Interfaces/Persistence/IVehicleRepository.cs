using Rento.Application.Vehicles.Common;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{

    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
        void Remove(Vehicle vehicle);
        Task<bool> ExistsWithRegistrationAsync(string registrationNumber);
        Task<bool> ExistsWithChassisAsync(string chassisNumber);
        Task<List<Vehicle>> GetFilteredAsync(GetAllVehiclesFilterQuery query, CancellationToken cancellationToken);
        Task<int> GetCount();
        Task<FilteredVehicleResult> GetAllForOwnerAsync(GetAllOwnerVehiclesQuery query, CancellationToken cancellationToken = default);

    }
}
