using CurrencyExchange.Application.Constants;
using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Application.Mappers;
using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Stores;
using Microsoft.Extensions.Logging;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace CurrencyExchange.Application.Services
{
    public class ExchangeRatesService(IExchangeRatesStore exchangeRatesStore, ICurrencyStore currencyStore, ILogger<ExchangeRatesService> logger) : IExchangeRatesService
    {
        private readonly IExchangeRatesStore _exchangeRatesStore = exchangeRatesStore;
        private readonly ICurrencyStore _currencyStore = currencyStore;

        public async Task<Result<IEnumerable<ExchangeRatesResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var exchangeRates = (await _exchangeRatesStore.GetAll(cancellationToken)).ToList().Select(r => r.MapToDto());
            return Result<IEnumerable<ExchangeRatesResponse>>.Success(exchangeRates);
        }

        public async Task<Result<ExchangeRatesResponse>> GetByCodesAsync(string codes, CancellationToken cancellationToken)
        {
            if(codes.Length != ApplicationConstants.CurrencyPairLength)
            {
                return Error.BadRequest("Неизвестные коды валют пары");
            }
            var baseCurrencyCode = codes.Substring(0, ApplicationConstants.CodeLength).ToUpper();
            var targetCurrencyCode = codes.Substring(3, ApplicationConstants.CodeLength).ToUpper();
            var result = await _exchangeRatesStore.GetByCodes(baseCurrencyCode, targetCurrencyCode, cancellationToken);
            if (result is null)
            {
                return Error.NotFound(string.Format("Курс обмена с кодами {0} и {1} не найден", baseCurrencyCode, targetCurrencyCode));
            }
            return result.MapToDto();
        }
        
        public async Task<Result<ExchangeRatesResponse>> AddAsync(ExchangeRatesRequest exchangeRatesRequest, CancellationToken cancellationToken)
        {
            var baseCurrencyCode = exchangeRatesRequest.BaseCurrencyCode;
            var targetCurrencyCode = exchangeRatesRequest.TargetCurrencyCode;
            var existExchangeRates = await _exchangeRatesStore.GetByCodes(baseCurrencyCode, targetCurrencyCode, cancellationToken);
            if (existExchangeRates is not null)
            {
                return Error.Conflict($"Курс обмена валют пары с кодами {baseCurrencyCode} и {targetCurrencyCode} уже существует");
            }
            var baseCurrency = await _currencyStore.GetByCode(baseCurrencyCode, cancellationToken);
            if (baseCurrency is null)
            {
                return Error.BadRequest($"Базовой валюты с кодом {baseCurrencyCode} не существует");
            }
            var targetCurrency = await _currencyStore.GetByCode(targetCurrencyCode, cancellationToken);
            if (targetCurrency is null)
            {
                return Error.BadRequest($"Целевой валюты с кодом {targetCurrencyCode} не существует");
            }
            var newExchangeRates = new ExchangeRates(baseCurrency.Id, targetCurrency.Id, exchangeRatesRequest.Rate);
            await _exchangeRatesStore.Add(newExchangeRates, cancellationToken);
            return new ExchangeRatesResponse(newExchangeRates.Id, baseCurrency.MapToDto(), targetCurrency.MapToDto(), newExchangeRates.Rate);
        }

        public async Task<Result<ExchangeRatesResponse>> UpdateRateAsync(string codes, decimal rate, CancellationToken cancellationToken)
        {
            if (codes.Length != ApplicationConstants.CurrencyPairLength)
            {
                return Error.BadRequest("Неизвестные коды валют пары");
            }
            if (rate < ApplicationConstants.MinimalRate)
            {
                return Error.BadRequest("Курс обмена валют должен быть больше 0");
            }
            var baseCurrencyCode = codes.Substring(0, 3).ToUpper();
            var targetCurrencyCode = codes.Substring(3, 3).ToUpper();
            var existExchangeRate = await _exchangeRatesStore.GetByCodes(baseCurrencyCode, targetCurrencyCode, cancellationToken);
            if (existExchangeRate is null)
            {
                return Error.NotFound(string.Format("Курс обмена с кодами {0} и {1} не найден", baseCurrencyCode, targetCurrencyCode));
            }
            var updatedExchangeRate = await _exchangeRatesStore.Update(baseCurrencyCode, targetCurrencyCode, rate, cancellationToken);
            return updatedExchangeRate.MapToDto();
        }
        public async Task<Result<ExchangeRatesWithAmount>> ExchangeAsync(string baseCurrencyCode, 
            string targetCurrencyCode, decimal amount, CancellationToken cancellationToken)
        {
            var existExchangeRate = await _exchangeRatesStore.GetByCodes(baseCurrencyCode, targetCurrencyCode, cancellationToken);
            if (existExchangeRate is not null)
            {
                return existExchangeRate.MapToDtoExchange(amount, (a, r) => a*r);
            }
            var reverseExchangeRate = await _exchangeRatesStore.GetByCodes(targetCurrencyCode, baseCurrencyCode, cancellationToken);
            if (reverseExchangeRate is not null)
            {
                return reverseExchangeRate.MapToDtoExchange(amount, (a, r) => a / r);
            }
            var UsdToBaseCurrencyRate = await _exchangeRatesStore.GetByCodes("USD", baseCurrencyCode, cancellationToken);
            if (UsdToBaseCurrencyRate is null)
            {
                return Error.BadRequest($"Курса между USD к {baseCurrencyCode} не существует");
            }
            var UsdToTargetCurrencyRate = await _exchangeRatesStore.GetByCodes("USD", targetCurrencyCode, cancellationToken);
            if (UsdToTargetCurrencyRate is null)
            {
                return Error.BadRequest($"Курса между USD к {targetCurrencyCode} не существует");
            }
            return (new ExchangeRates(
                UsdToBaseCurrencyRate.TargetCurrencyId,
                UsdToTargetCurrencyRate.TargetCurrencyId,
                UsdToTargetCurrencyRate.Rate / UsdToBaseCurrencyRate.Rate
                )
            ).MapToDtoExchange(amount, (a, r) => a * r);
        }
    }
}
