using Vesting.Worker.Domain;

namespace UnitTest.Worker.Domain
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
            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData("not-found.csv", 1)]
        [InlineData("test.csv", -1)]
        [InlineData("test.csv", 7)]
        public void ShouldValidationFail(string fileName, int digits)
        {
            // Arrange
            var input = new Input(fileName, DateTime.Now, digits);
        
            // Act
            var result = InputValidator.Execute(input);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}