namespace Rento.Application.Vehicles.Common
{
    public record FilteredVehicleResult(
        List<VehicleResult> Vehicles,
        int TotalCount
    );
}
