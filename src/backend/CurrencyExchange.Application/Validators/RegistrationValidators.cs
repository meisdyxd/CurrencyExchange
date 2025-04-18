using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Application.Validators
{
    public static class RegistrationValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CurrencyRequest>, CurrencyRequestValidator>();
            services.AddScoped<IValidator<ExchangeRatesRequest>, ExchangeRatesValidator>();
            return services;
        }
    }
}
