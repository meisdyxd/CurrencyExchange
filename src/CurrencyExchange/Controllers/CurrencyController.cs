using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.HttpResult;
using ResultSharp.Logging;
using LogLevel = ResultSharp.Logging.LogLevel;

namespace CurrencyExchange.Presentation.Controllers
{
    [Route("api/сurrency")]
    [ApiController]
    public class CurrencyController(ICurrencyService currencyService) : ControllerBase
    {
        private readonly ICurrencyService _currencyService = currencyService;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CancellationTokenSource tokenSource = new();
            var result = _currencyService.GetAllCurrenciesAsync(tokenSource.Token);
            tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            CancellationTokenSource tokenSource = new();
            var result = _currencyService.GetByCodeAsync(code, tokenSource.Token);
            tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CurrencyRequest currencyRequest)
        {
            CancellationTokenSource tokenSource = new();
            var result = _currencyService.AddAsync(currencyRequest, tokenSource.Token);
            tokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            return (await result)
                .LogErrorMessages(logLevel: LogLevel.Warning)
                .ToResponse();
        }
    }
}
