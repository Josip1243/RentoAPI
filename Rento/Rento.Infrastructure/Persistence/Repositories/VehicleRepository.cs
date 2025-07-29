using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
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

        public async Task<List<Vehicle>> GetFilteredAsync(GetAllVehiclesFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Vehicles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(v =>
                    v.Brand.Contains(request.Search) || v.Model.Contains(request.Search));
            }

            if (!string.IsNullOrWhiteSpace(request.FuelType))
            {
                query = query.Where(v => v.FuelType == request.FuelType);
            }

            if (request.MinYear.HasValue)
            {
                query = query.Where(v => v.Year >= request.MinYear.Value);
            }

            if (request.MaxYear.HasValue)
            {
                query = query.Where(v => v.Year <= request.MaxYear.Value);
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(v => v.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(v => v.Price <= request.MaxPrice.Value);
            }

            // Sortiranje
            query = request.SortBy?.ToLower() switch
            {
                "price" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(v => v.Price)
                    : query.OrderBy(v => v.Price),

                "year" => request.SortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(v => v.Year)
                    : query.OrderBy(v => v.Year),

                _ => query.OrderBy(v => v.Id)
            };

            // Paginacija
            int skip = (request.PageNumber - 1) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
