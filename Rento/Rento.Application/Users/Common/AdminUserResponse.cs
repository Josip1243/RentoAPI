namespace Rento.Application.Users.Common
{
    public class AdminUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? Oib { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DriversLicenceNumber { get; set; }
        public string Role { get; set; } = default!;
        public bool Verified { get; set; }
    }
}
