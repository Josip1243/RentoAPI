namespace Rento.Application.Vehicles.Common
{
    public record VehicleResult(
        int Id,
        string Brand,
        string Model,
        int Year,
        string RegistrationNumber,
        string ChassisNumber,
        string FuelType,
        int DoorsNumber,
        int SeatsNumber,
        decimal Price,
        int OwnerId,
        List<string> ImageUrls
    );
}
