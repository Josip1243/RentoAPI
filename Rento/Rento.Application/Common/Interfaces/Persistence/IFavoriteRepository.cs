using Rento.Domain.Entities;
using System.Security.Cryptography;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId);
        void Add(Favorite favorite);
        void Remove(Favorite favorite);
        Task<Favorite?> GetAsync(int userId, int vehicleId);
        Task<bool> ExistsAsync(int userId, int vehicleId);
    }
}
