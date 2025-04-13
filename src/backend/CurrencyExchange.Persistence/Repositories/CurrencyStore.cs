using CurrencyExchange.Domain.Models;
using CurrencyExchange.Domain.Stores;
using CurrencyExchange.Persistence.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Persistence.Repositories
{
    public class CurrencyStore(CurrencyExchangeDbContext context, ILogger<CurrencyStore> logger) : ICurrencyStore
    {
        private readonly CurrencyExchangeDbContext _context = context;
        private readonly ILogger<CurrencyStore> _logger = logger;

        public async Task<Currency> Add(Currency currency, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Добавление новой валюты с кодом \"{currency.Code}\"");
            await _context.AddAsync(currency);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation($"Добавлена новая валюта с кодом \"{currency.Code}\" и Id {currency.Id}");
            return currency;
        }

        public async Task<IQueryable<Currency>> GetAll(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Получение списка валют");
            return await Task.FromResult(_context.Currencies.AsNoTracking().AsQueryable());
        }

        public async Task<Currency?> GetByCode(string code, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Получение валюты с кодом \"{code}\"");
            return await _context.Currencies.AsNoTracking().FirstOrDefaultAsync(c => c.Code == code, cancellationToken);
        }
    }
}
