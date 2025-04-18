using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public interface IDbContextOptionsConfigurator<TDbContext> where TDbContext: DbContext
    {
        public void Configure(DbContextOptionsBuilder<TDbContext> builder);
    }
}
