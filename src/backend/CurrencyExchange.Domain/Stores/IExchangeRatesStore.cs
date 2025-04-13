using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Domain.Stores
{
    /// <summary>
    /// Описывает работу с курсами валют
    /// </summary>
    public interface IExchangeRatesStore
    {
        /// <summary>
        /// Получить список всех обменных курсов
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список всех обменных курсов</returns>
        Task<IQueryable<ExchangeRates>> GetAll(CancellationToken cancellationToken);
        /// <summary>
        /// Получить конкретный обменный курс. Валютная пара задаётся идущими подряд кодами валют в адресе запроса.
        /// </summary>
        /// <param name="codeBaseCurrency">Код базовой валюты</param>
        /// <param name="codeTargetCurrency">Код целевой валюты</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Обменный курс</returns>
        Task<ExchangeRates?> GetByCodes(string codeBaseCurrency, string codeTargetCurrency, CancellationToken cancellationToken);
        /// <summary>
        /// Получить обменный курс по коду базовой валюты
        /// </summary>
        /// <param name="codeBaseCurrency">Код базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список обменных курсов, с указанным кодом базовой валюты</returns>
        Task<IQueryable<ExchangeRates>> GetByBaseCurrencyCode(string codeBaseCurrency, CancellationToken cancellationToken);
        /// <summary>
        /// Получить обменный курс по коду целевой валюты
        /// </summary>
        /// <param name="codeTargetCurrency">Код целевой валюты</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список обменных курсов, с указанным кодом целевой валюты</returns>
        Task<IQueryable<ExchangeRates>> GetByTargetCurrencyCode(string codeTargetCurrency, CancellationToken cancellationToken);
        /// <summary>
        /// Добавить обменный курс
        /// </summary>
        /// <param name="exchangeRates">Курс обмена</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Обменный курс</returns>
        Task<ExchangeRates> Add(ExchangeRates exchangeRates, CancellationToken cancellationToken);
        /// <summary>
        /// Обновление 
        /// </summary>
        /// <param name="codeBaseCurrency"></param>
        /// <param name="codeTargetCurrency"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ExchangeRates> Update(string codeBaseCurrency, string codeTargetCurrency, decimal rate, CancellationToken cancellationToken);
    }
}
