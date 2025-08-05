namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicles { get; }
        IReservationRepository Reservations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
