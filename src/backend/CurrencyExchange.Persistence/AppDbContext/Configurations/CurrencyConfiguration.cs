using CurrencyExchange.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.Persistence.AppDbContext.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Code)
                .IsUnique();

            builder.Property(c => c.Code)
                .HasMaxLength(3);

            builder.Property(c => c.FullName).HasMaxLength(32);

            builder.Property(c => c.Sign).HasMaxLength(8);

            builder.HasMany(c => c.BaseCurrencyRates)
                .WithOne(r => r.BaseCurrency)
                .HasForeignKey(r => r.BaseCurrencyId);

            builder.HasMany(c => c.TargetCurrencyRates)
                .WithOne(r => r.TargetCurrency)
                .HasForeignKey(r => r.TargetCurrencyId);
        }
    }
}
