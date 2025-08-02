using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review?> GetByReservationIdAsync(int reservationId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId, cancellationToken);
        }

        public async Task AddAsync(Review review, CancellationToken cancellationToken = default)
        {
            await _context.Reviews.AddAsync(review, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Review>> GetByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews
                .Where(r => r.VehicleId == vehicleId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task DeleteAsync(Review review, CancellationToken cancellationToken = default)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
