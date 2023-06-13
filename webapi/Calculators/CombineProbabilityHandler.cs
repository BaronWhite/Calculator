using LB.Demos.CalculatorWebApi.Interfaces;

namespace LB.Demos.CalculatorWebApi.Calculators;

public class CombineProbabilityHandler : ICalculationHandler
{
    public double Calculate(double a, double b) => a + b - (a * b);
}