namespace Rento.Contracts.Reservations
{
    public record CreateReservationRequest(
        int UserId,
        int VehicleId,
        DateTime StartDate,
        DateTime EndDate
    );
}
