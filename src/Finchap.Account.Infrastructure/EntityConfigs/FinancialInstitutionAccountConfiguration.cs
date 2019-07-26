using Finchap.Account.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finchap.Account.Infrastructure.EntityConfigs
{
  internal class FinancialInstitutionAccountConfiguration : IEntityTypeConfiguration<FIAccount>
  {
    public void Configure(EntityTypeBuilder<FIAccount> builder)
    {
      builder.ToTable("FinancialInstitutionAccount");

      builder.HasKey(ct => ct.Id);

      builder.Property(ct => ct.Id)
          .HasDefaultValue(1)
          .ValueGeneratedOnAdd()
          .IsRequired();

      builder.Property(ct => ct.Description)
          .HasMaxLength(200);

      builder.Property(ct => ct.UserId)
          .IsRequired();

      builder.HasMany(ct => ct.Accounts);

      builder.Property(ct => ct.Institution)
          .IsRequired();

      builder.Ignore(ct => ct.HasUpdatedSecrets);
    }
  }
}