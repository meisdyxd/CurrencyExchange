using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Application.Mappers;
using CurrencyExchange.Domain.Stores;
using Microsoft.Extensions.Logging;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace CurrencyExchange.Application.Services
{
    public class CurrencyService(ICurrencyStore currencyStore, ILogger<CurrencyService> logger): ICurrencyService
    {
        private readonly ICurrencyStore _currencyStore = currencyStore;

        public async Task<Result<IEnumerable<CurrencyResponse>>> GetAllCurrenciesAsync(CancellationToken cancellationToken)
        {
            var currencies = (await _currencyStore.GetAll(cancellationToken)).ToList();
            return Result<IEnumerable<CurrencyResponse>>.Success(currencies.Select(c => c.MapToDto()));
        }
        public async Task<Result<CurrencyResponse>> GetByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var currency = await _currencyStore.GetByCode(code, cancellationToken);
            if (currency is null)
            {
                return Error.BadRequest($"Валюта с кодом {code} не найдена");
            }
            return currency.MapToDto();
        }
        public async Task<Result<CurrencyResponse>> AddAsync(CurrencyRequest currencyRequest, CancellationToken cancellationToken)
        {
            var existCurrency = await _currencyStore.GetByCode(currencyRequest.Code, cancellationToken);
            if (existCurrency is not null)
            {
                return Error.Conflict($"Создание валюты с кодом {currencyRequest.Code} невозможно. Валюта с таким кодом существует");
            }
            var newCurrency = currencyRequest.MapToEntity();
            await _currencyStore.Add(newCurrency, cancellationToken);
            return newCurrency.MapToDto();
        }
    }
}
