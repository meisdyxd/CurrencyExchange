using ResultSharp.Errors;

namespace CurrencyExchange.Domain.Models
{
    public class ExchangeRates
    {
        /// <summary>
        /// Первичный ключ курса обмена в формате guid
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// Внешний ключ базовой валюты
        /// </summary>
        public Guid BaseCurrencyId { get; private set; }
        /// <summary>
        /// Внешний ключ целевой валюты
        /// </summary>
        public Guid TargetCurrencyId { get; private set; }
        /// <summary>
        /// Курс обмена единицы базовой валюты к единице целевой валюты
        /// </summary>
        public decimal Rate {  get; private set; }

        public Currency BaseCurrency { get; private set; } = null!;
        public Currency TargetCurrency { get; private set; } = null!;

        /// <summary>
        /// Конструктор курса обмена
        /// </summary>
        /// <param name="baseCurrencyId">Id базовой валюты</param>
        /// <param name="targetCurrencyId">Id целевой валюты</param>
        /// <param name="rate">Курс</param>
        public ExchangeRates(Guid baseCurrencyId, Guid targetCurrencyId, decimal rate)
        {
            Id = Guid.NewGuid();
            BaseCurrencyId = baseCurrencyId;
            TargetCurrencyId = targetCurrencyId;
            Rate = rate;
        }
        /// <summary>
        /// Конструктор курса обмен для EF Core
        /// </summary>
        protected ExchangeRates() { }

        /// <summary>
        /// Обновление курса обмена
        /// </summary>
        /// <param name="rate">Курс</param>
        public void UpdateRate(decimal rate)
        {
            Validate(BaseCurrencyId, TargetCurrencyId, rate);
            Rate = rate;
        }
        /// <summary>
        /// Валидация курса обмена
        /// </summary>
        /// <param name="baseCurrencyId">Код базовой валюты</param>
        /// <param name="targetCurrencyId">Код целевой валюты</param>
        /// <param name="rate">Курс</param>
        private static List<Error> Validate(Guid baseCurrencyId, Guid targetCurrencyId, decimal rate)
        {
            List<Error> errors = new();
            if (baseCurrencyId == targetCurrencyId)
            {
                errors.Add(Error.Validation("Id базовой валюты не должен совпадать с Id целевой валюты"));
            }
            if (rate <= 0)
            {
                errors.Add(Error.Validation("Курс обмена не должен быть <= 0"));
            }
            return errors;
        }
    }
}
