using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Domain.Models;

namespace CurrencyExchange.Application.Mappers
{
    public static class CurrencyMapper
    {
        public static CurrencyResponse MapToDto(this Currency currency)
        {
            return new CurrencyResponse(currency.Id, currency.Code, currency.FullName, currency.Sign);
        }
        public static Currency MapToEntity(this CurrencyRequest currencyRequest)
        {
            return new Currency(currencyRequest.Code, currencyRequest.FullName, currencyRequest.Sign);
        }
    }
}
