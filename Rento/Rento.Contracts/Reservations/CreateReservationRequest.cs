namespace Rento.Contracts.Reservations
{
    public record CreateReservationRequest(
        int VehicleId,
        DateTime StartDate,
        DateTime EndDate,

        string CardNumber,
        string Expiry,
        string Cvc
    );
}
