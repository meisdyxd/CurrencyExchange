using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Application.Validators;
using CurrencyExchange.Presentation.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.HttpResult;
using ResultSharp.Logging;
using LogLevel = ResultSharp.Logging.LogLevel;

namespace CurrencyExchange.Presentation.Controllers
{
    [Route("api/currency")]
    [ApiController]
    public class CurrencyController(ICurrencyService currencyService, IValidator<CurrencyRequest> validator) : ControllerBase
    {
        private readonly ICurrencyService _currencyService = currencyService;
        private readonly IValidator<CurrencyRequest> _validator = validator;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CancellationTokenSource tokenSource = new();
            var result = _currencyService.GetAllCurrenciesAsync(tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            CancellationTokenSource tokenSource = new();
            var result = _currencyService.GetByCodeAsync(code, tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CurrencyRequest currencyRequest)
        {   
            CancellationTokenSource tokenSource = new();
            var token = tokenSource.Token;
            var validationResult = await _validator.ValidateAsync(currencyRequest, token);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = await _currencyService.AddAsync(currencyRequest, token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return result
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
    }
}
