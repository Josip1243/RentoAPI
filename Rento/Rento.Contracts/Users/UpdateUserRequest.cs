namespace Rento.Contracts.Users
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? Oib { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DriversLicenceNumber { get; set; }
        public string Role { get; set; } = default!;
    }

}
