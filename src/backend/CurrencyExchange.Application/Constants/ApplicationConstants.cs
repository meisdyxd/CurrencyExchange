namespace CurrencyExchange.Application.Constants
{
    public static class ApplicationConstants
    {
        /// <summary>
        /// Длина кода валют 3 базовая валюта + 3 целевая валюта
        /// </summary>
        public const int CurrencyPairLength = 6;
        /// <summary>
        /// Минимальный курс
        /// </summary>
        public const decimal MinimalRate = 0.00m;
        /// <summary>
        /// Длина кода
        /// </summary>
        public const int CodeLength = 3;
    }
}
