using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{

    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
        void Remove(Vehicle vehicle);
    }
}
