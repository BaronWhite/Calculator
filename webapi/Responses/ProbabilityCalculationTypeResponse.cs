using LB.Demos.CalculatorWebApi.Enums;

namespace LB.Demos.CalculatorWebApi.Responses;

/// <summary>
/// Describe a supported calculation.
/// </summary>
/// <param name="CalculationType">Type identifier</param>
/// <param name="Display">Display name of calculation</param>
/// <param name="Description">Description of the calculation</param>
public record ProbabilityCalculationTypeResponse(ProbabilityCalculations.CalculationType CalculationType, string Display, string Description);