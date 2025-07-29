namespace Rento.Contracts.Vehicles
{
    public record VehicleFilterRequest(
        string? Search,             
        string? FuelType,
        int? MinYear,
        int? MaxYear,
        decimal? MinPrice,
        decimal? MaxPrice,
        string? SortBy,             
        string? SortDirection,
        int PageNumber = 1,
        int PageSize = 10
    );
}
