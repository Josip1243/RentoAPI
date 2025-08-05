namespace Rento.Application.Reservations.Common
{
    public record ReservationDetailsResult(
    int Id,
    DateTime ReservationDate,
    DateTime StartDate,
    DateTime EndDate,
    string Status,
    DateTime CreatedAt,
    VehicleDetailsResult Vehicle
);

    public record VehicleDetailsResult(
        int Id,
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay,
        List<string> ImageUrls
    );
}
