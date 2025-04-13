using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public class ExchangeRatesValidator: AbstractValidator<ExchangeRatesRequest>
    {
        public ExchangeRatesValidator()
        {
            RuleFor(r => r.baseCurrencyCode)
                .NotEmpty().WithMessage("Базовый код валюты не может быть пустым").WithErrorCode("400")
                .Length(3).WithMessage("Длина кода базовой валюты должна быть равна 3 символам")
                .Matches("^[A-Z]+$").WithMessage("Код должен состоять из заглавных букв");
            RuleFor(r => r.targetCurrencyCode)
                .NotEmpty().WithMessage("Целевой код валюты не может быть пустым").WithErrorCode("400")
                .Length(3).WithMessage("Длина кода целевой валюты должна быть равна 3 символам")
                .Matches("^[A-Z]+$").WithMessage("Код валюты должен состоять из заглавных букв");
            RuleFor(r => r.rate)
                .NotEmpty().WithMessage("Курс не может быть пустым")
                .GreaterThan(0).WithMessage("Курс обмена валют должен быть больше 0");
        }
    }
}
