using CurrencyExchange.Application.DTOs.CurrencyDTOs;

namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesWithAmount(CurrencyResponse BaseCurrency, CurrencyResponse TargetCurrency, decimal Rate, decimal Amount, decimal ConvertedAmount);
}
