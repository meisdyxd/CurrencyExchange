using CurrencyExchange.Domain.Models;
using CurrencyExchange.Persistence.AppDbContext.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public class CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options) : DbContext(options)
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRates> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
