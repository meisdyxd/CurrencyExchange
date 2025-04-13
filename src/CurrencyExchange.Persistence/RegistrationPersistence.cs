using CurrencyExchange.Domain.Stores;
using CurrencyExchange.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Persistence
{
    public static class RegistrationPersistence
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            return services
                .AddCurrencyStore()
                .AddExchangeRatesStore();
        }
        private static IServiceCollection AddCurrencyStore(this IServiceCollection services)
        {
            return services.AddScoped<ICurrencyStore, CurrencyStore>();
        }
        private static IServiceCollection AddExchangeRatesStore(this IServiceCollection services)
        {
            return services.AddScoped<IExchangeRatesStore, ExchangeRatesStore>();
        }
    }
}
