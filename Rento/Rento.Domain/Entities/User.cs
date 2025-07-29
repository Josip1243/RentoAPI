using Rento.Domain.Enums;

namespace Rento.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Oib { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? Address { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;
        public string? DriversLicenceNumber { get; set; } = default!;
        public UserRole Role { get; set; } = UserRole.Customer;
        public bool Verified { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    }
}
