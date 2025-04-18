using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public class ApplicationDbContextConfigurator : DbContextOptionsConfigurator<CurrencyExchangeDbContext>
    {
        protected override string ConnectionStringInSettings { get; } = "Postgres";
        public ApplicationDbContextConfigurator(ILoggerFactory loggerFactory, IConfiguration configuration) : base(loggerFactory, configuration)
        {
        }
    }
}
