using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesResponse(Guid Id, CurrencyResponse BaseCurrency, CurrencyResponse TargetCurrency, decimal Rate);
}
