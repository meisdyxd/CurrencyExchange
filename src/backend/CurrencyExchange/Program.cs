using CurrencyExchange.Application;
using CurrencyExchange.Application.DTOs.CurrencyDTOs;
using CurrencyExchange.Application.Validators;
using CurrencyExchange.Persistence;
using CurrencyExchange.Persistence.AppDbContext;
using CurrencyExchange.Presentation.Extensions;
using CurrencyExchange.Presentation.Middleware;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

var configuration = builder.Configuration;

services.AddControllers();

services.AddEndpointsApiExplorer();
services.AddSwagger();

services
    .AddPersistence()
    .AddApplication();

builder.Services.AddScoped<IValidator<CurrencyRequest>, CurrencyRequestValidator>();


var app = builder.Build();

app.ApplyMigrations();

app.UseMiddleware<RequestTimingMiddleware>();

app.UseResultSharpLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
