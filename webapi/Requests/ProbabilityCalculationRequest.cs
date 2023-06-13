using FluentValidation;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;

namespace LB.Demos.CalculatorWebApi.Requests;

/// <summary>
/// Parameters to execute a calculation.
/// </summary>
/// <param name="A">First parameter</param>
/// <param name="B">Second parameter</param>
/// <param name="CalculationType">Type of calculation to execute</param>
public record ProbabilityCalculationRequest(double A, double B, ProbabilityCalculations.CalculationType CalculationType)
{
    // I wouldn't usually do this, but...
    // For small dtos/validators this might makes it easier to maintain
    // looks a bit untidy though
    public class Validator : AbstractValidator<ProbabilityCalculationRequest>
    {
        public Validator(ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType> calculationHandlerFactory)
        {
            RuleFor(request => request.A).InclusiveBetween(0, 1);
            RuleFor(request => request.B).InclusiveBetween(0, 1);
            RuleFor(request => request.CalculationType)
                .IsInEnum()
                // Fail early, heavy usage might make this a problem
                .Must(calculationHandlerFactory.IsCalculationsSupported)
                .WithMessage("Calculation type [{PropertyValue}] not yet supported.");
        }
    }
}