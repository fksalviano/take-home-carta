using Application.Commons.Domain;
using Application.UseCases.ReadFile.Extensions;
using AutoFixture;
using FluentAssertions;

namespace UnitTest.Application.UseCases.ReadFile;

public class ReadFileExtensionsTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Fact]
    public void ShouldConvertToOutput()
    {
        // Arrange
        var vestingEvents = _fixture.Build<VestingEvent>().CreateMany(1);
        
        // Act
        var result = vestingEvents.ToOutput();

        // Assert
        result.Should().NotBeNull();
        result.VestingEvents.Should().NotBeNullOrEmpty();
    }
}
