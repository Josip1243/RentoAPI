namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IVehicleRepository Vehicles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
