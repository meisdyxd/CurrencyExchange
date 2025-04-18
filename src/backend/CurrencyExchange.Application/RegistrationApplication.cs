using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Application.Services;
using CurrencyExchange.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Application
{
    public static class RegistrationApplication
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddCurrencyService()
                .AddExchangeRatesService()
                .AddValidators();
        }
        private static IServiceCollection AddCurrencyService(this IServiceCollection services)
        {
            return services.AddScoped<ICurrencyService, CurrencyService>();
        }
        private static IServiceCollection AddExchangeRatesService(this IServiceCollection services)
        {
            return services.AddScoped<IExchangeRatesService, ExchangeRatesService>();
        }
    }
}
