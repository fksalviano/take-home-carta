using FluentAssertions;
using Application.Commons.Domain;
using Application.Commons.Domain.Validators;
using Application.Commons.Extensions;

namespace UnitTest.Application.Extensions
{
    public class ValidationExtensionsTests
    {
        [Fact]
        public void ShouldConvertToDomainResult()
        {
            // Arrrange
            var input = GetInvalidInput();
            var sut = new InputValidator().Validate(input);

            // Act
            var result = sut.ToDomainResult();

            // Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(sut.IsValid);
            result.Error.Should().Be(string.Join(", ", sut.Errors.Select(_ => _.ErrorMessage)));
        }

        private static InputArguments GetInvalidInput() =>
            new InputArguments(string.Empty, DateTime.Now);
    }
}