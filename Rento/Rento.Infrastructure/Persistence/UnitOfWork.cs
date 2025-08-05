using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleImageRepository _vehicleImageRepository;
        private readonly IReservationRepository _reservationRepository;

        public UnitOfWork(ApplicationDbContext context, IVehicleRepository vehicleRepository, IReservationRepository reservationRepository, IVehicleImageRepository vehicleImageRepository)
        {
            _context = context ;
            _vehicleRepository = vehicleRepository;
            _reservationRepository = reservationRepository;
            _vehicleImageRepository = vehicleImageRepository;
        }

        public IVehicleRepository Vehicles => _vehicleRepository;
        public IVehicleImageRepository VehicleImages => _vehicleImageRepository;
        public IReservationRepository Reservations => _reservationRepository;

        public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
            _context.SaveChangesAsync(ct);
    }
}
