namespace Rento.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = default!;
        public Vehicle Vehicle { get; set; } = default!;
    }
}
