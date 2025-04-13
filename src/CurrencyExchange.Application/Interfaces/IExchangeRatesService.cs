using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using ResultSharp.Core;

namespace CurrencyExchange.Application.Interfaces
{
    public interface IExchangeRatesService
    {
        Task<Result<ExchangeRatesResponse>> AddAsync(ExchangeRatesRequest exchangeRatesRequest, CancellationToken cancellationToken);
        Task<Result<ExchangeRatesResponse>> GetByCodesAsync(string codes, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ExchangeRatesResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ExchangeRatesResponse>> UpdateRateAsync(string codes, decimal rate, CancellationToken cancellationToken);
        Task<Result<ExchangeRatesWithAmount>> ExchangeAsync(string baseCurrencyCode,
            string targetCurrencyCode, decimal amount, CancellationToken cancellationToken);
    }
}
