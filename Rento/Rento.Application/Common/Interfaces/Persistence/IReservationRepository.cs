using Rento.Application.Reservations.Common;
using Rento.Application.Vehicles.Common;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Interfaces.Persistence
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Reservation>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
        void Remove(Reservation reservation);
        Task<List<Reservation>> GetReservationsByVehicleIdAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<ReservationDetailsResult?> GetReservationDetailsByIdAsync(int reservationId, CancellationToken cancellationToken = default);
        Task<List<OwnerReservationResult>> GetReservationsForOwnerAsync(int ownerId, CancellationToken cancellationToken = default);
        Task<Reservation?> GetReservationWithVehicleAsync(int reservationId, CancellationToken cancellationToken = default);
        Task<bool> AnyForVehicleAsync(int vehicleId, CancellationToken cancellationToken = default);
        Task<bool> HasCompletedReservation(int userId, int vehicleId);
        Task<List<Reservation>> GetAllWithUserAndVehicleAsync();
        Task<List<BusyDateRangeDto>> GetBusyDateRangesForVehicleAsync(int vehicleId, DateTime fromDate);
        void AddPayment(Payment payment);
        void AddOwnerPayout(OwnerPayout payout);

    }
}
