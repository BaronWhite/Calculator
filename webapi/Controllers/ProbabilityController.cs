using FluentValidation;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;
using LB.Demos.CalculatorWebApi.Requests;
using LB.Demos.CalculatorWebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace LB.Demos.CalculatorWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProbabilityController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType> _calculationHandlerFactory;
    private readonly AbstractValidator<ProbabilityCalculationRequest> _probabilityRequestValidator;

    public ProbabilityController(ILogger logger,
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
        _probabilityRequestValidator.ValidateAndThrow(calculation);
        ICalculationHandler handler = _calculationHandlerFactory.GetHandler(calculation.CalculationType);
        double result = handler.Calculate(calculation.A, calculation.B);
        _logger.Information("Performed calculation: A [{a}] B [{b}] Type [{type}] Result [{result}]",
            calculation.A, calculation.B, calculation.CalculationType.ToString(), result);
        return result;
    }
}