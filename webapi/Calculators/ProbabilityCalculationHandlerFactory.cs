using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;

namespace LB.Demos.CalculatorWebApi.Calculators;

public class ProbabilityCalculationHandlerFactory : ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType>
{
    private static readonly IReadOnlyDictionary<ProbabilityCalculations, ICalculationHandler>
        CalculationHandlers = new Dictionary<ProbabilityCalculations, ICalculationHandler>()
        {
            { ProbabilityCalculations.Either, new EitherProbabilityHandler() },
            { ProbabilityCalculations.Combine, new CombineProbabilityHandler() },
        };

    private static readonly IEnumerable<ProbabilityCalculations.CalculationType> CalculationTypes =
        CalculationHandlers.Keys.Select(calculations => calculations.Type);

    public bool IsCalculationsSupported(ProbabilityCalculations.CalculationType calculationType) => CalculationTypes.Contains(calculationType);

    public IEnumerable<ProbabilityCalculations> GetSupportedCalculations() => CalculationHandlers.Keys;

    public ICalculationHandler GetHandler(ProbabilityCalculations.CalculationType calculationType)
    {
        // We are exchanging lookup quickness for design simplicity, wouldn't change unless there's an SLO
        ICalculationHandler handler = CalculationHandlers.SingleOrDefault(pair => pair.Key.Type == calculationType).Value;

        if (handler is null)
        {
            throw new NotImplementedException($"The received calculation {calculationType} is not supported.");
        }

        return handler;
    }
}