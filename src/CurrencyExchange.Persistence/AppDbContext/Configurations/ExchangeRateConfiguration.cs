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
        }
    }
}
