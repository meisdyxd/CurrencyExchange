using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesResponse(Guid Id, Currency BaseCurrency, Currency TargetCurrency, decimal rate);
}
