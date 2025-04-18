using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public abstract class DbContextOptionsConfigurator<TDbContext>(ILoggerFactory loggerFactory, IConfiguration configuration) : IDbContextOptionsConfigurator<TDbContext> where TDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory = loggerFactory;
        private readonly IConfiguration _configuration = configuration;

        protected abstract string ConnectionStringInSettings { get; }

        public void Configure(DbContextOptionsBuilder<TDbContext> builder)
        {
            var connectionString = _configuration.GetConnectionString(ConnectionStringInSettings);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Connection string {ConnectionStringInSettings} is not found");
            }
            else
            {
                builder.UseLoggerFactory(_loggerFactory)
                    .UseNpgsql(connectionString, x => x.CommandTimeout(60));
            }
        }
    }

}
