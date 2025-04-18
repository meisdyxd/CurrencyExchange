using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Application.Mappers
{
    public static class ExchangeRatesMapper
    {
        public static ExchangeRatesResponse MapToDto(this ExchangeRates exchangeRates)
        {
            return new ExchangeRatesResponse(exchangeRates.Id, exchangeRates.BaseCurrency.MapToDto(), exchangeRates.TargetCurrency.MapToDto(), exchangeRates.Rate);
        }
        /*public static ExchangeRates MapToDto(this ExchangeRatesRequest exchangeRates)
        {
            return new ExchangeRates(exchangeRates.);
        }*/
        public static ExchangeRatesWithAmount MapToDtoExchange(this ExchangeRates exchangeRates, decimal amount, Func<decimal, decimal, decimal> exchangeAction)
        {
            return new ExchangeRatesWithAmount(exchangeRates.BaseCurrency.MapToDto(), exchangeRates.TargetCurrency.MapToDto(), exchangeRates.Rate, amount, exchangeAction(amount, exchangeRates.Rate));
        }
    }
}
