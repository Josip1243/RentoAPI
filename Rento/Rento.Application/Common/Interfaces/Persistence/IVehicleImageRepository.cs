using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IVehicleImageRepository
    {
        Task<VehicleImage?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        void Remove(VehicleImage image);
        Task FixOrderForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default);

    }

}
