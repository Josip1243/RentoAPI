namespace Rento.Contracts.Vehicles
{
    public record VehicleResponse(
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
        int OwnerId
    );
}
