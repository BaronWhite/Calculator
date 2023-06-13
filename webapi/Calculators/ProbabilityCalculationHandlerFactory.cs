using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;

namespace LB.Demos.CalculatorWebApi.Calculators;

public class ProbabilityCalculationHandlerFactory : ICalculationHandlerFactory<ProbabilityCalculations>
{
    private static readonly IReadOnlyDictionary<ProbabilityCalculations, ICalculationHandler>
        CalculationHandlers = new Dictionary<ProbabilityCalculations, ICalculationHandler>()
        {
            { ProbabilityCalculations.Either, new EitherProbabilityHandler() },
            { ProbabilityCalculations.Combine, new CombineProbabilityHandler() },
        };

    public IEnumerable<ProbabilityCalculations> GetSupportedCalculations() => CalculationHandlers.Keys;

    public ICalculationHandler GetHandler(int calculationType)
    {
        // We are exchanging lookup quickness for design simplicity, wouldn't change unless there's an SLO
        ICalculationHandler handler = CalculationHandlers.SingleOrDefault(pair => pair.Key.Type == (ProbabilityCalculations.CalculationType)calculationType).Value;

        if (handler is null)
        {
            throw new NotImplementedException($"The received calculation {calculationType} is not supported.");
        }

        return handler;
    }
}