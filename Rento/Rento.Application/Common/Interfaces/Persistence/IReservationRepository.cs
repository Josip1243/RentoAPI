using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Reservation>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
        void Remove(Reservation reservation);
        Task<List<Reservation>> GetReservationsByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);


    }
}
