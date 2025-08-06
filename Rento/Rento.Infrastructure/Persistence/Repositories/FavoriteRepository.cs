using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public void Add(Favorite favorite) => _context.Favorites.Add(favorite);

        public void Remove(Favorite favorite) => _context.Favorites.Remove(favorite);

        public async Task<Favorite?> GetAsync(int userId, int vehicleId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.VehicleId == vehicleId);
        }

        public async Task<bool> ExistsAsync(int userId, int vehicleId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.VehicleId == vehicleId);
        }
    }
}
