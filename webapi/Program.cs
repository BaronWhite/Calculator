using LB.Demos.CalculatorWebApi.Calculators;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType>, ProbabilityCalculationHandlerFactory>();

var app = builder.Build();

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
