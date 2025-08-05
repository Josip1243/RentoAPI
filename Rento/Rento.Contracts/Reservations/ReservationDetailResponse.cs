namespace Rento.Contracts.Reservations
{
    public record ReservationDetailsResponse(
        int Id,
        DateTime ReservationDate,
        DateTime StartDate,
        DateTime EndDate,
        string Status,
        DateTime CreatedAt,
        VehicleDetails Vehicle
    );

    public record VehicleDetails(
        int Id,
        string Brand,
        string Model,
        int Year,
        decimal PricePerDay,
        List<string> ImageUrls
    );
}
