using FluentAssertions;
using Vesting.Application.Commons.Domain;
using Vesting.Application.Commons.Extensions;

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

        private static Input GetInvalidInput() =>
            new Input(string.Empty, DateTime.Now);
    }
}