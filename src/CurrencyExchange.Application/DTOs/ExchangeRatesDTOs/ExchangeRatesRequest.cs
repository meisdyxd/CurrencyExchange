namespace CurrencyExchange.Application.DTOs.ExchangeRatesDTOs
{
    public record ExchangeRatesRequest(string baseCurrencyCode, string targetCurrencyCode, decimal rate);
}
