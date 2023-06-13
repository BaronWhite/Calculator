using AutoFixture;
using AutoFixture.AutoMoq;
using FluentValidation.TestHelper;
using LB.Demos.CalculatorWebApi.Enums;
using LB.Demos.CalculatorWebApi.Interfaces;
using LB.Demos.CalculatorWebApi.Requests;
using Moq;

namespace WebApi.Tests.Requests
{
    public class ProbabilityCalculationRequestValidator
    {
        private ProbabilityCalculationRequest.Validator _sut;
        private readonly IFixture _fixture;
        private readonly Mock<ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType>> _handler;
        const ProbabilityCalculations.CalculationType notInEnumType = (ProbabilityCalculations.CalculationType)666;
        private static readonly Random Random = new();

        public ProbabilityCalculationRequestValidator()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Customize<ProbabilityCalculationRequest>(composer => composer
                .With(request => request.A, () => Random.NextDouble())
                .With(request => request.B, () => Random.NextDouble())
                .With(request => request.CalculationType, ProbabilityCalculations.CalculationType.Combine)
            );

            _handler = _fixture.Freeze<Mock<ICalculationHandlerFactory<ProbabilityCalculations, ProbabilityCalculations.CalculationType>>>();
            _handler
                .Setup(factory => factory.IsCalculationsSupported(It.IsAny<ProbabilityCalculations.CalculationType>()))
                .Returns<ProbabilityCalculations.CalculationType>(type => type is ProbabilityCalculations.CalculationType.Combine or notInEnumType);

            _sut = _fixture.Create<ProbabilityCalculationRequest.Validator>();
        }

        public static TheoryData<double> InvalidParams =>
            new()
            {
                -0.1,
                -1,
                1.1,
                2,
                double.MinValue,
                double.MaxValue,
            };

        [Fact]
        public void Validate_Valid_NoErrors()
        {
            var request = _fixture.Create<ProbabilityCalculationRequest>();

            var result = _sut.TestValidate(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(InvalidParams))]
        public void Validate_InvalidA_HasError(double value)
        {
            var request = _fixture.Create<ProbabilityCalculationRequest>() with
            {
                A = value
            };

            var result = _sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(calc => calc.A);
        }

        [Theory]
        [MemberData(nameof(InvalidParams))]
        public void Validate_InvalidB_HasError(double value)
        {
            var request = _fixture.Create<ProbabilityCalculationRequest>() with
            {
                B = value
            };

            var result = _sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(calc => calc.B);
        }

        [Fact]
        public void Validate_UnsupportedType_HasError()
        {
            var request = _fixture.Create<ProbabilityCalculationRequest>() with
            {
                CalculationType = ProbabilityCalculations.CalculationType.Either
            };

            var result = _sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(calc => calc.CalculationType);
        }

        [Fact]
        public void Validate_NotInEnumType_HasError()
        {
            var request = _fixture.Create<ProbabilityCalculationRequest>() with
            {
                CalculationType = notInEnumType
            };

            var result = _sut.TestValidate(request);

            result.ShouldHaveValidationErrorFor(calc => calc.CalculationType);
        }
    }
}