using FluentValidation;
using LB.Demos.CalculatorWebApi.Calculators;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;
using LB.Demos.CalculatorWebApi.Requests;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType>, ProbabilityCalculationHandlerFactory>();
builder.Services.AddScoped<AbstractValidator<ProbabilityCalculationRequest>, ProbabilityCalculationRequest.Validator>();

// Create logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/calculations.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Services.AddSingleton(Log.Logger);


var app = builder.Build();

// Add services to the container.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
