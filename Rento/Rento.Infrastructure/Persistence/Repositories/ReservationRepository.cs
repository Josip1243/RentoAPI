using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;
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

        public async Task<ReservationDetailsResult?> GetReservationDetailsByIdAsync(int reservationId, CancellationToken cancellationToken = default)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Vehicle)
                    .ThenInclude(v => v.Images)
                .FirstOrDefaultAsync(r => r.Id == reservationId, cancellationToken);

            if (reservation is null)
                return null;

            var vehicle = reservation.Vehicle;

            return new ReservationDetailsResult(
                reservation.Id,
                reservation.ReservationDate,
                reservation.StartDate,
                reservation.EndDate,
                reservation.Status.ToString(),
                reservation.CreatedAt,
                new VehicleDetailsResult(
                    vehicle.Id,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.Price,
                    vehicle.Images.Select(i => i.Url).ToList()
                )
            );
        }

        public async Task<List<OwnerReservationResult>> GetReservationsForOwnerAsync(int ownerId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Include(r => r.Vehicle)
                .Include(r => r.User)
                .Where(r => r.Vehicle.OwnerId == ownerId)
                .Select(r => new OwnerReservationResult(
                    r.Id,
                    r.VehicleId,
                    r.Vehicle.Brand,
                    r.Vehicle.Model,
                    r.UserId,
                    r.User.FirstName,
                    r.User.LastName,
                    r.StartDate,
                    r.EndDate,
                    r.Status.ToString()
                ))
                .ToListAsync(cancellationToken);
        }

        public async Task<Reservation?> GetReservationWithVehicleAsync(int reservationId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == reservationId, cancellationToken);
        }

        public async Task<bool> AnyForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .AnyAsync(r => r.VehicleId == vehicleId, cancellationToken);
        }

    }
}
