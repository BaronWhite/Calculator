using LB.Demos.CalculatorWebApi.Enums;

namespace LB.Demos.CalculatorWebApi.Requests;

/// <summary>
/// Parameters to execute a calculation.
/// </summary>
/// <param name="A">First parameter</param>
/// <param name="B">Second parameter</param>
/// <param name="CalculationType">Type of calculation to execute</param>
public record ProbabilityCalculationRequest(double A, double B, ProbabilityCalculations.CalculationType CalculationType);