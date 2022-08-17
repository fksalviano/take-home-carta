using FluentAssertions;
using Application.Commons.Domain;

namespace UnitTest.Application.Domain
{
    public class InputValidatorTests
    {
        [Fact]
        public void ShouldValidateSuccess()
        {
            // Arrange
            var input = new Input("test.csv", DateTime.Now, 1);

            // Act
            var result = InputValidator.Execute(input);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("not-found.csv", 1)]
        [InlineData("", 1)]
        [InlineData("test.csv", -1)]
        [InlineData("test.csv", 7)]
        public void ShouldValidationFail(string fileName, int digits)
        {
            // Arrange
            var input = new Input(fileName, DateTime.Now, digits);
        
            // Act
            var result = InputValidator.Execute(input);

            // Assert
            result.IsValid.Should().BeFalse();;
        }
    }
}