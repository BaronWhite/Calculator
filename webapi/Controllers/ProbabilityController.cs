using FluentValidation;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;
using LB.Demos.CalculatorWebApi.Requests;
using LB.Demos.CalculatorWebApi.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LB.Demos.CalculatorWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProbabilityController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType> _calculationHandlerFactory;
    private readonly AbstractValidator<ProbabilityCalculationRequest> _probabilityRequestValidator;

    public ProbabilityController(ILogger<WeatherForecastController> logger,
        ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType> calculationHandlerFactory,
        AbstractValidator<ProbabilityCalculationRequest> probabilityRequestValidator)
    {
        _logger = logger;
        _calculationHandlerFactory = calculationHandlerFactory;
        _probabilityRequestValidator = probabilityRequestValidator;
    }

    [HttpGet("supported-calculations")]
    public IOrderedEnumerable<ProbabilityCalculationTypeResponse> GetSupportedProbabilityCalculations()
    {
        _logger.LogDebug("{class}.{method} start.", nameof(ProbabilityController), nameof(GetSupportedProbabilityCalculations));

        IOrderedEnumerable<ProbabilityCalculationTypeResponse> calculations = _calculationHandlerFactory.GetSupportedCalculations()
            .Select(calc => new ProbabilityCalculationTypeResponse(
                CalculationType: calc.Type,
                Display: calc.Display,
                Description: calc.Description))
            .OrderBy(calc => calc.Display);
        return calculations;
    }

    /// <summary>
    /// Executes a calculation on the provided probabilities and returns the result
    /// </summary>
    /// <param name="calculation">Calculation parameters</param>
    /// <returns></returns>
    [HttpPost("calculate")]
    public double CalculateProbability([FromBody] ProbabilityCalculationRequest calculation)
    {
        // I actually dislike this log style, I think it's what the stack trace and exceptions are for
        // If you need to dig through logs for this info, ouch, but I've seen it used and even parameters included,
        // wonder what your thoughts are
        _logger.LogDebug("{class}.{method} start.", nameof(ProbabilityController), nameof(CalculateProbability));

        _probabilityRequestValidator.ValidateAndThrow(calculation);
        ICalculationHandler handler = _calculationHandlerFactory.GetHandler(calculation.CalculationType);
        double result = handler.Calculate(calculation.A, calculation.B);
        return result;
    }
}