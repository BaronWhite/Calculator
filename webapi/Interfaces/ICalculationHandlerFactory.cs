using LB.Demos.CalculatorWebApi.Enums;

namespace LB.Demos.CalculatorWebApi.Interfaces;

public interface ICalculationHandlerFactory<TCalculations> where TCalculations : ICalculations
{
    IEnumerable<TCalculations> GetSupportedCalculations();
    ICalculationHandler GetHandler(int calculationType);
}