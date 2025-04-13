using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Domain.Stores
{
    /// <summary>
    /// Описывает работу с валютами
    /// </summary>
    public interface ICurrencyStore
    {
        /// <summary>
        /// Получить список всех валют
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список всех валют</returns>
        Task<IQueryable<Currency>> GetAll(CancellationToken cancellationToken);
        /// <summary>
        /// Получить валюту по коду
        /// </summary>
        /// <param name="code">Строка с кодом валюты, например "EUR"</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Валюта с указанным кодом</returns>
        Task<Currency?> GetByCode(string code, CancellationToken cancellationToken);
        /// <summary>
        /// Добавить валюту
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<Currency> Add(Currency currency, CancellationToken cancellationToken);
    }
}
