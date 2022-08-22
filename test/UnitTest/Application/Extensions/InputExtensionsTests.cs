using Application.Commons.Domain;
using Application.Commons.Extensions;
using AutoFixture;
using FluentAssertions;

namespace UnitTest.Application.Extensions;

public class InputExtensionsTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ShouldConvertToReadFileInput()
    {
        // Arrange
        var input = _fixture.Create<InputArguments>();

        // Act
        var result = input.ToReadFileInput();

        // Assert
        result.Should().NotBeNull();
    }
}
