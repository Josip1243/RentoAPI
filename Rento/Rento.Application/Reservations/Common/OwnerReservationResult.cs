namespace Rento.Application.Reservations.Common
{
    public record OwnerReservationResult(
        int Id,
        int VehicleId,
        string VehicleBrand,
        string VehicleModel,
        int RenterId,
        string RenterFirstName,
        string RenterLastName,
        DateTime StartDate,
        DateTime EndDate,
        string Status
    );
}
