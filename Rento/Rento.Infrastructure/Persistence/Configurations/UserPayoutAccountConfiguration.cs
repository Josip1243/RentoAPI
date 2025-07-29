using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rento.Domain.Entities;

namespace Rento.Infrastructure.Persistence.Configurations
{
    public class UserPayoutAccountConfiguration : IEntityTypeConfiguration<UserPayoutAccount>
    {
        public void Configure(EntityTypeBuilder<UserPayoutAccount> builder)
        {
            builder.ToTable("UserPayoutAccounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Iban)
                .IsRequired()
                .HasMaxLength(34);

            builder.Property(a => a.BankName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.AccountHolderName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.CreatedAt)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); // keep payout accounts even if user is deleted
        }
    }
}
