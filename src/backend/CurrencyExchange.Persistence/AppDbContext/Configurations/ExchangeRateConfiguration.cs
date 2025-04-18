using CurrencyExchange.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.Persistence.AppDbContext.Configurations
{
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRates>
    {
        public void Configure(EntityTypeBuilder<ExchangeRates> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Rate)
                .HasPrecision(18, 6);

            builder.HasOne(r => r.BaseCurrency)
                .WithMany(c => c.BaseCurrencyRates)
                .HasForeignKey(r => r.BaseCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.TargetCurrency)
                .WithMany(c => c.TargetCurrencyRates)
                .HasForeignKey(r => r.TargetCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
