using CurrencyExchange.Application.DTOs.ExchangeRatesDTOs;
using CurrencyExchange.Application.Interfaces;
using CurrencyExchange.Presentation.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.HttpResult;
using ResultSharp.Logging;
using LogLevel = ResultSharp.Logging.LogLevel;

namespace CurrencyExchange.Presentation.Controllers
{
    [Route("api/exchangeRates")]
    [ApiController]
    public class ExchangeRatesController(IExchangeRatesService exchangeRatesService, IValidator<ExchangeRatesRequest> validator) : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService = exchangeRatesService;
        private readonly IValidator<ExchangeRatesRequest> _validator = validator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CancellationTokenSource tokenSource = new();
            var result = _exchangeRatesService.GetAllAsync(tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpGet("{codes}")]
        public async Task<IActionResult> GetByCodes(string codes)
        {
            CancellationTokenSource tokenSource = new();
            var result = _exchangeRatesService.GetByCodesAsync(codes, tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ExchangeRatesRequest exchangeRatesRequest)
        {
            CancellationTokenSource tokenSource = new();
            var token = tokenSource.Token;
            var validationResult = await _validator.ValidateAsync(exchangeRatesRequest, token);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var result = _exchangeRatesService.AddAsync(exchangeRatesRequest, tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpPatch("{codes}")]
        public async Task<IActionResult> UpdateRate(string codes, decimal rate)
        {
            CancellationTokenSource tokenSource = new();
            var result = _exchangeRatesService.UpdateRateAsync(codes, rate, tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpGet("exchange")]
        public async Task<IActionResult> Exchange(string from, string to, decimal amount)
        {
            CancellationTokenSource tokenSource = new();
            var result = _exchangeRatesService.ExchangeAsync(from, to, amount, tokenSource.Token);
            tokenSource.CancelAfter(CommonConstants.TimeAfterCancel);
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
    }
}
