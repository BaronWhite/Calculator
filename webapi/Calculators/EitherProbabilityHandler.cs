using LB.Demos.CalculatorWebApi.Interfaces;

namespace LB.Demos.CalculatorWebApi.Calculators;

public class EitherProbabilityHandler : ICalculationHandler
{
    public double Calculate(double a, double b) => a * b;
}