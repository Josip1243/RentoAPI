using ErrorOr;
using MediatR;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GetAllVehiclesFilter
{
    public record GetAllVehiclesFilterQuery(
        string? Search,
        string? FuelType,
        int? MinYear,
        int? MaxYear,
        decimal? MinPrice,
        decimal? MaxPrice,
        string? SortBy,
        string? SortDirection,
        int PageNumber,
        int PageSize
    ) : IRequest<ErrorOr<List<VehicleResponse>>>;
}
