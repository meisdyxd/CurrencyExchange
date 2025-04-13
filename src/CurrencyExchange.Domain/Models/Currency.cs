using ResultSharp.Errors;

namespace CurrencyExchange.Domain.Models
{
    public class Currency
    {
        /// <summary>
        /// Первичный ключ валюты в формате guid
        /// </summary>
        public Guid Id { get; private set; }
        
        /// <summary>
        /// Код валюты, к примеру "EUR"
        /// </summary>
        public string Code { get; private set; }
        
        /// <summary>
        /// Полное имя валюты, к примеру "Euro"
        /// </summary>       
        public string FullName { get; private set; }
        
        /// <summary>
        /// Символ валюты, к примеру "€"
        /// </summary>
        public string Sign { get; private set; }

        public List<ExchangeRates> ExchangeRates { get; private set; } = new();

        
        /// <summary>
        /// Конструктор валюты
        /// </summary>
        /// <param name="code">Код валюты</param>
        /// <param name="fullName">Полное имя валюты</param>
        /// <param name="sign">Знак валюты</param>
        public Currency(string code, string fullName, string sign)
        {
            Id = Guid.NewGuid();
            Code = code;
            FullName = fullName;
            Sign = sign;
        }
        
        /// <summary>
        /// Конструктор валюты для EF Core
        /// </summary>
        protected Currency() { }

        /// <summary>
        /// Валидация валюты
        /// </summary>
        /// <param name="code">Код валюты</param>
        /// <param name="fullName">Полное имя валюты</param>
        /// <param name="sign">Знак валюты</param>
        /// <exception cref="ArgumentException">Ошибка при валидации</exception>
        public static List<Error> Validate(string code, string fullName, string sign)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(code))
            {
                errors.Add(Error.Validation("Код валюты не должен быть пустым"));
            }
            else
            {
                if (code.Length != 3)
                {
                    errors.Add(Error.Validation("Длина кода валюты должна быть 3 символа"));
                }

                if (code.Any(c => !char.IsLetter(c) || !char.IsUpper(c)))
                {
                    errors.Add(Error.Validation("Код валюты должен состоять только из заглавных букв"));
                }
            }
            if (string.IsNullOrWhiteSpace(fullName))
            {
                errors.Add(Error.Validation("Полное имя валюты не должно быть пустым"));
            }
            if (string.IsNullOrWhiteSpace(sign))
            {
                errors.Add(Error.Validation("Знак валюты не должен быть пустым"));
            }
            return errors;
        }
    }
}
