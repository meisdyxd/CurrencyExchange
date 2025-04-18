using Microsoft.OpenApi.Models;

namespace CurrencyExchange.Presentation.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency API", Version = "v1" });
            });
        }
    }
}
