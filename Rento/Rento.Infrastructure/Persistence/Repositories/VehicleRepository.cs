using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .Include(v => v.Images)
                .Include(v => v.Unavailabilities)
                .Include(v => v.Reviews)
                .Include(v => v.Reservations)
                .Include(v => v.Favorites)
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<List<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .Include(v => v.Images)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            await _context.Vehicles.AddAsync(vehicle, cancellationToken);
        }

        public void Remove(Vehicle vehicle)
        {
            _context.Vehicles.Remove(vehicle);
        }

        public async Task<bool> ExistsWithRegistrationAsync(string registrationNumber)
        {
            return await _context.Vehicles.AnyAsync(v => v.RegistrationNumber == registrationNumber);
        }

        public async Task<bool> ExistsWithChassisAsync(string chassisNumber)
        {
            return await _context.Vehicles.AnyAsync(v => v.ChassisNumber == chassisNumber);
        }
    }
}
