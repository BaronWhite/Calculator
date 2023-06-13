using FluentAssertions;
using LB.Demos.CalculatorWebApi.Calculators;

namespace WebApi.Tests.Calculations
{
    public class EitherProbabilityHandlerTests
    {
        private readonly EitherProbabilityHandler _sut;

        public EitherProbabilityHandlerTests()
        {
            _sut = new EitherProbabilityHandler();
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(0.25, 0.5, 0.125)]
        [InlineData(0.5, 0.5, 0.25)]
        public void Calculate_ReturnsExpected(double a, double b, double expected)
        {
            var actual = _sut.Calculate(a, b);
            actual.Should().Be(expected);
        }
    }
}