namespace Rento.Contracts.Users
{
    public class BecomeOwnerRequest
    {
        public string Oib { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Iban { get; set; } = default!;
        public string AccountHolderName { get; set; } = default!;
        public string BankName { get; set; } = default!;

    }
}
