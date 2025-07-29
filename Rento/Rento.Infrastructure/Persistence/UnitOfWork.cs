using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;
        
        public UnitOfWork(ApplicationDbContext context, IVehicleRepository vehicleRepository)
        {
            _context = context ;
            _vehicleRepository = vehicleRepository;
        }

        public IVehicleRepository Vehicles => _vehicleRepository;

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _context.SaveChangesAsync(ct);
    }
}
