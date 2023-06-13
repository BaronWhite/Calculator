using FluentAssertions;
using LB.Demos.CalculatorWebApi.Calculators;
using LB.Demos.CalculatorWebApi.Enums;

namespace WebApi.Tests.Calculations;

public class ProbabilityCalculationHandlerFactoryTests
{
    private readonly ProbabilityCalculationHandlerFactory _sut;

    public ProbabilityCalculationHandlerFactoryTests()
    {
        _sut = new ProbabilityCalculationHandlerFactory();
    }

    [Fact]
    public void GetSupportedCalculations_ReturnsSupportedCalculations()
    {
        var expected = new[]
        {
            ProbabilityCalculations.Combine,
            ProbabilityCalculations.Either,
        };

        var actual = _sut.GetSupportedCalculations();

        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData(ProbabilityCalculations.CalculationType.Combine, typeof(CombineProbabilityHandler))]
    [InlineData(ProbabilityCalculations.CalculationType.Either, typeof(EitherProbabilityHandler))]
    public void GetHandler_ReturnsExpectedHandler(ProbabilityCalculations.CalculationType calculationType, Type expectedHandler)
    {
        var actual = _sut.GetHandler(calculationType);

        actual.Should().BeOfType(expectedHandler);
    }

    [Theory]
    [InlineData(ProbabilityCalculations.CalculationType.Combine, true)]
    [InlineData(ProbabilityCalculations.CalculationType.Either, true)]
    [InlineData((ProbabilityCalculations.CalculationType)5, false)]
    public void IsCalculationsSupported_ReturnsExpectedResult(ProbabilityCalculations.CalculationType calculationType, bool expected)
    {
        var actual = _sut.IsCalculationsSupported(calculationType);

        actual.Should().Be(expected);
    }
}