using CurrencyExchange.Persistence.AppDbContext.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistence.AppDbContext.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyCurrencyConfiguration()
                .ApplyExchangeRatesConfiguration();
            return modelBuilder;
        }
        private static ModelBuilder ApplyCurrencyConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            return modelBuilder;
        }
        private static ModelBuilder ApplyExchangeRatesConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExchangeRateConfiguration());
            return modelBuilder;
        }
    }
}
