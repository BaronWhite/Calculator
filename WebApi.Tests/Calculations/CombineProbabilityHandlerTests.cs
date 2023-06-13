using FluentAssertions;
using LB.Demos.CalculatorWebApi.Calculators;

namespace WebApi.Tests.Calculations;

public class CombineProbabilityHandlerTests
{
    private readonly CombineProbabilityHandler _sut;

    public CombineProbabilityHandlerTests()
    {
        // Usually use Autofixture to freeze and setup mocks
        _sut = new CombineProbabilityHandler();
    }

    // I usually work with data in the form of strings and use Autofixture,
    // unsure what the best practice for maths is, but some hardcoded known results sounds good
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(0.25, 0.5, 0.625)]
    [InlineData(0.5, 0.5, 0.75)]
    public void Calculate_ReturnsExpected(double a, double b, double expected)
    {
        var actual = _sut.Calculate(a, b);
        actual.Should().Be(expected);
    }
}