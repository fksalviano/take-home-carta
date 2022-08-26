using FluentAssertions;
using Application.Commons.Extensions;
using AutoFixture;

namespace UnitTest.Application.Extensions
{
    public class ValidationExtensionsTests
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void ShouldConvertToDomainResult()
        {
            // Arrange            
            var sut = _fixture.Create<FluentValidation.Results.ValidationResult>();

            // Act
            var result = sut.ToDomainResult();

            // Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(sut.IsValid);
            result.Error.Should().Be(string.Join(", ", sut.Errors.Select(_ => _.ErrorMessage)));
        }
     }
}