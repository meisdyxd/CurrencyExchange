using CurrencyExchange.Domain.Models;
using CurrencyExchange.Persistence.AppDbContext.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public class CurrencyExchangeDbContext: DbContext
    {
        public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options): base(options) { }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRates> exchangeRates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyAllConfigurations();
        }
    }
}
