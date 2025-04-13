using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using ResultSharp.Core;

namespace CurrencyExchange.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<IEnumerable<CurrencyResponse>>> GetAllCurrenciesAsync(CancellationToken cancellationToken);
        Task<Result<CurrencyResponse>> GetByCodeAsync(string code, CancellationToken cancellationToken);
        Task<Result<CurrencyResponse>> AddAsync(CurrencyRequest currencyRequest, CancellationToken cancellationToken);
    }
}
