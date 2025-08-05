using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehicleRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                .Include(v => v.Images.OrderBy(img => img.Order))
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

        public async Task<bool> ExistsWithRegistrationAsync(string registrationNumber, int? excludeVehicleId = null)
        {
            return await _context.Vehicles.AnyAsync(v => v.RegistrationNumber == registrationNumber &&
                (!excludeVehicleId.HasValue || v.Id != excludeVehicleId.Value));
        }

        public async Task<bool> ExistsWithChassisAsync(string chassisNumber)
        {
            return await _context.Vehicles.AnyAsync(v => v.ChassisNumber == chassisNumber);
        }

        public async Task<bool> ExistsWithChassisAsync(string chassisNumber, int? excludeVehicleId = null)
        {
            return await _context.Vehicles
                .AnyAsync(v =>
                    v.ChassisNumber == chassisNumber &&
                    (!excludeVehicleId.HasValue || v.Id != excludeVehicleId.Value));
        }

        public async Task<List<Vehicle>> GetFilteredAsync(GetAllVehiclesFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Vehicles.Include(v => v.Images).AsQueryable();

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

                _ => query
            };

            // Paginacija
            int skip = (request.PageNumber) * request.PageSize;
            query = query.Skip(skip).Take(request.PageSize);

            var result = await query.ToListAsync(cancellationToken);

            return result;
        }

        public async Task<int> GetCount()
        {
            return await _context.Vehicles.CountAsync();
        }

        public async Task<FilteredVehicleResult> GetAllForOwnerAsync(
            GetAllOwnerVehiclesQuery query,
            CancellationToken cancellationToken = default)
        {
            var vehiclesQuery = _context.Vehicles
                .Include(v => v.Images)
                .Where(v => v.OwnerId == query.OwnerId)
                .AsQueryable();

            // Filteri
            if (!string.IsNullOrWhiteSpace(query.FuelType))
                vehiclesQuery = vehiclesQuery.Where(v => v.FuelType.ToLower() == query.FuelType.ToLower());

            if (query.MinYear.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.Year >= query.MinYear);

            if (query.MaxYear.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.Year <= query.MaxYear);

            if (query.MinPrice.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.Price >= query.MinPrice);

            if (query.MaxPrice.HasValue)
                vehiclesQuery = vehiclesQuery.Where(v => v.Price <= query.MaxPrice);

            // Sortiranje
            var isDesc = string.Equals(query.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);

            vehiclesQuery = query.SortBy?.ToLower() switch
            {
                "brand" => isDesc ? vehiclesQuery.OrderByDescending(v => v.Brand) : vehiclesQuery.OrderBy(v => v.Brand),
                "model" => isDesc ? vehiclesQuery.OrderByDescending(v => v.Model) : vehiclesQuery.OrderBy(v => v.Model),
                "price" => isDesc ? vehiclesQuery.OrderByDescending(v => v.Price) : vehiclesQuery.OrderBy(v => v.Price),
                "year" => isDesc ? vehiclesQuery.OrderByDescending(v => v.Year) : vehiclesQuery.OrderBy(v => v.Year),
                _ => vehiclesQuery.OrderBy(v => v.Id)
            };

            var totalCount = await vehiclesQuery.CountAsync(cancellationToken);

            var vehicles = await vehiclesQuery
                .Skip((query.PageNumber) * query.PageSize)
                .Take(query.PageSize)
                .Select(v => _mapper.Map<VehicleResult>(v))
                .ToListAsync(cancellationToken);

            return new FilteredVehicleResult(vehicles, totalCount);
        }

        public async Task<Vehicle?> GetByIdWithImagesAsync(int vehicleId, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .Include(v => v.Images)
                .FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);
        }

    }
}
