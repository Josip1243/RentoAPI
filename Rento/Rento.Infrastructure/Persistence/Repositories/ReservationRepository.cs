using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<List<Reservation>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Where(r => r.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            await _context.Reservations.AddAsync(reservation, cancellationToken);
        }

        public void Remove(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
        }

        public async Task<List<Reservation>> GetReservationsByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Where(r => r.VehicleId == vehicleId)
                .ToListAsync(cancellationToken);
        }

    }
}
