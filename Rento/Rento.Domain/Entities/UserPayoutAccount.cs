namespace Rento.Domain.Entities
{
    public class UserPayoutAccount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Iban { get; set; } = default!;
        public string BankName { get; set; } = default!;
        public string AccountHolderName { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = default!;
    }
}
