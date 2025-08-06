using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IReviewRepository
    {
        Task AddAsync(Review review, CancellationToken cancellationToken = default);
        Task<List<Review>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Review review, CancellationToken cancellationToken = default);
        Task<bool> HasUserReviewedVehicle(int reviewerId, int vehicleId);
        Task<List<Review>> GetAllWithVehicleAndReviewerAsync();
        void Delete(Review review);
    }
}
