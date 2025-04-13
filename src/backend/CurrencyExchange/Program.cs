using CurrencyExchange.Application;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.AppDbContext;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

var configuration = builder.Configuration;

services.AddControllers();
services.AddPersistence();
services.AddApplication();
services.AddAppDbContext(configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
