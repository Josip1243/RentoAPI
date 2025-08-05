namespace Rento.Domain.Entities
{
    public class VehicleImage
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? Description { get; set; }
        public int Order { get; set; }
        public Vehicle Vehicle { get; set; } = default!;
    }
}
