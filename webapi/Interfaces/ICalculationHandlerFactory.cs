using LB.Demos.CalculatorWebApi.Enums;

namespace LB.Demos.CalculatorWebApi.Interfaces;

public interface ICalculationHandlerFactory<out TCalculations, in TEnum>
    where TCalculations : ICalculations
    where TEnum : Enum
{
    bool IsCalculationsSupported(TEnum calculationType);
    IEnumerable<TCalculations> GetSupportedCalculations();
    ICalculationHandler GetHandler(TEnum calculationType);
}