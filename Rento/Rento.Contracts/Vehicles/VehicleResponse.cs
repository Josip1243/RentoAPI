namespace Rento.Contracts.Vehicles
{
    public record VehicleListResponse(
        List<VehicleResponse> Vehicles,
        int TotalCount
    );

    public record VehicleImageDto(
        int Id,
        int Order,
        string Url);

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
        int OwnerId,
        List<VehicleImageDto> Images
    );
}
