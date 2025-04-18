using ResultSharp.Configuration;
using ResultSharp.Logging.MicrosoftLogger;

namespace CurrencyExchange.Presentation.Extensions
{
    public static class ResultSharpExtensions
    {
        public static void UseResultSharpLogging(this WebApplication application)
        {
            new ResultConfigurationGlobal().Configure(options =>
            {
                options.EnableLogging = true;
                options.LoggingConfiguration.Configure(config =>
                {
                    config.LoggingAdapter = new MicrosoftLoggingAdapter(application.Logger);
                });
            });
        }
    }
}
