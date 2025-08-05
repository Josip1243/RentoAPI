namespace Rento.Contracts.Vehicles
{

    public record UpdateVehicleImageOrderRequest(List<ImageOrderDto> Images);

    public record ImageOrderDto(int ImageId, int Order);
}
