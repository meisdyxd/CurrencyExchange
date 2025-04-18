using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Persistence.AppDbContext
{
    public static class DbContextDI
    {
        public static IServiceCollection AddAppDbContext<TDbContext, TDbContextConfigurator>(this IServiceCollection services)
            where TDbContext : DbContext
            where TDbContextConfigurator: class, IDbContextOptionsConfigurator<TDbContext>
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContextPool<TDbContext>(Configure<TDbContext>);

            services.AddSingleton<IDbContextOptionsConfigurator<TDbContext>, TDbContextConfigurator>()
                .AddScoped<DbContext>(provider => provider.GetRequiredService<TDbContext>());

            return services;
        }
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CurrencyExchangeDbContext>();
            db.Database.Migrate();
        }
        private static void Configure<TDbContext>(IServiceProvider sp, DbContextOptionsBuilder builder)
        where TDbContext : DbContext
        {
            var configurator = sp.GetRequiredService<IDbContextOptionsConfigurator<TDbContext>>();
            configurator.Configure((DbContextOptionsBuilder<TDbContext>)builder);
        }
    }
}
