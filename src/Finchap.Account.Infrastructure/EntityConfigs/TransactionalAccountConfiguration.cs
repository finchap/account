using Finchap.Account.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Finchap.Account.Infrastructure.EntityConfigs
{
  internal class TransactionalAccountConfiguration : IEntityTypeConfiguration<TRAccount>
  {
    public void Configure(EntityTypeBuilder<TRAccount> builder)
    {
      builder.ToTable("TransactionalAccounts");

      builder.HasKey(ct => ct.Id);

      builder.Property(ct => ct.Id)
          .ValueGeneratedNever()
          .IsRequired();

      builder.Property(ct => ct.Description)
          .HasMaxLength(200);

      builder.Property(ct => ct.UserId)
          .IsRequired();

      builder.Property(ct => ct.AccountNumber)
          .HasMaxLength(200)
          .IsRequired();

      builder.HasOne<TRAccount>();

      builder.Property(ct => ct.AccountType)
          .HasConversion(
              v => v.ToString(),
              v => (AccountType)Enum.Parse(typeof(AccountType), v));

      builder.Property(ct => ct.LastRefresh);
    }
  }
}