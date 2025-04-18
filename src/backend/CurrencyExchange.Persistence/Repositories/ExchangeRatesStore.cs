using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Stores;
using CurrencyExchange.Persistence.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Persistence.Repositories
{
    public class ExchangeRatesStore(CurrencyExchangeDbContext context, ILogger<ExchangeRatesStore> logger) : IExchangeRatesStore
    {
        private readonly CurrencyExchangeDbContext _context = context;
        private readonly ILogger<ExchangeRatesStore> _logger = logger;

        public async Task<ExchangeRates> Add(ExchangeRates exchangeRates, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Добавление курса обмена с Id {exchangeRates.Id}");
            await _context.ExchangeRates.AddAsync(exchangeRates, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Добавлен курс обмена с Id {exchangeRates.Id}");
            return exchangeRates;
        }

        public async Task<IQueryable<ExchangeRates>> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Получение списка с курсом обмена");
            return await Task.FromResult(_context.ExchangeRates
                .AsNoTracking()
                .Include(r => r.BaseCurrency)
                .Include(r => r.TargetCurrency)
                .AsQueryable());
        }

        public async Task<ExchangeRates?> GetByCodes(string codeBaseCurrency, string codeTargetCurrency, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Получение курса обмена между {codeBaseCurrency} и {codeTargetCurrency}");
            return await _context.ExchangeRates
                .AsNoTracking()
                .Include(r => r.BaseCurrency)
                .Include(r => r.TargetCurrency)
                .FirstOrDefaultAsync(r => r.BaseCurrency.Code == codeBaseCurrency && r.TargetCurrency.Code == codeTargetCurrency, cancellationToken);
        }

        public async Task<IQueryable<ExchangeRates>> GetByBaseCurrencyCode(string codeBaseCurrency, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Получение курса обмена по коду базовой валюты {codeBaseCurrency}");
            return await Task.FromResult(_context.ExchangeRates
                .AsNoTracking()
                .Include(r => r.BaseCurrency)
                .Where(r => r.BaseCurrency.Code == codeBaseCurrency)
                .AsQueryable());
        }

        public async Task<IQueryable<ExchangeRates>> GetByTargetCurrencyCode(string codeTargetCurrency, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Получение курса обмена по коду целевой валюты {codeTargetCurrency}");
            return await Task.FromResult(_context.ExchangeRates
                .AsNoTracking()
                .Include(r => r.TargetCurrency)
                .Where(r => r.TargetCurrency.Code == codeTargetCurrency)
                .AsQueryable());
        }

        public async Task<ExchangeRates> Update(string codeBaseCurrency, string codeTargetCurrency, decimal rate, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Обновление курса обмена между {codeBaseCurrency} и {codeTargetCurrency}");
            var exchangeRate = await _context.ExchangeRates
                .Include(r => r.BaseCurrency)
                .Include(r => r.TargetCurrency)
                .FirstAsync(r => r.BaseCurrency.Code == codeBaseCurrency && r.TargetCurrency.Code == codeTargetCurrency ,cancellationToken);
            exchangeRate.UpdateRate(rate);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Курс обмена между {codeBaseCurrency} и {codeTargetCurrency} обновлен");
            return exchangeRate;
        }
    }
}
