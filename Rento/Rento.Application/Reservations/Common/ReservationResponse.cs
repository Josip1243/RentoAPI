namespace Rento.Application.Reservations.Common
{
    public record ReservationResponse(
    int Id,
    int UserId,
    int VehicleId,
    DateTime ReservationDate,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    DateTime CreatedAt
);
}
