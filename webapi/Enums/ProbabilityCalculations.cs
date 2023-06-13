using static LB.Demos.CalculatorWebApi.Enums.ProbabilityCalculations;

namespace LB.Demos.CalculatorWebApi.Enums;

public record ProbabilityCalculations(CalculationType Type, string Display, string Description) : ICalculations
{
    public static readonly ProbabilityCalculations Combine = new(CalculationType.Combine, "Combine With", "P(A)P(B)");

    public static readonly ProbabilityCalculations Either = new(CalculationType.Either, "Either", "P(A) + P(B) – P(A)P(B)");

    public enum CalculationType
    {
        Combine,
        Either
    }

    int ICalculations.GetType => (int)Type;
}

public interface ICalculations
{
    string Display { get; }
    string Description { get; }
    int GetType { get; }
}