using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesWithAmount(Currency BaseCurrency, Currency TargetCurrency, decimal Rate, decimal Amount, decimal ConvertedAmount);
}
