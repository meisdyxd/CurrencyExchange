namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesRequest(string BaseCurrencyCode, string TargetCurrencyCode, decimal Rate);
}
