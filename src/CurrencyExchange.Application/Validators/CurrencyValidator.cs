using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using FluentValidation;

namespace CurrencyExchange.Application.Validators
{
    public class CurrencyRequestValidator: AbstractValidator<CurrencyRequest>
    {
        public CurrencyRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Код валюты не должен быть пустым")
                .Length(3).WithMessage("Длина кода должна быть 3 символа")
                .Matches("^[A-Z]+$").WithMessage("Код валюты должен состоять из заглавных букв");
            RuleFor(x => x.Sign)
                .NotEmpty().WithMessage("Знак валюты не должен быть пустым");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Полное имя валюты не должно быть пустым");
        }
    }
}
