using Application.Commons.Domain;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Extensions;
using AutoFixture;
using FluentAssertions;

namespace UnitTest.Application.UseCases.GetVested;

public class GetVestedExtensionsTests
{
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public void ShouldConvertToOutput()
    {
        // Arrange
        var digits = 1;
        var vestedSchedules = _fixture.Build<VestedShedule>().CreateMany(1);
        
        // Act
        var result = vestedSchedules.ToOutput(digits);

        // Assert
        result.Should().NotBeNull();
        result.VestedShedules.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldConvertToCSV()
    {
        // Arrange
        var digits = 1;
        var vestedSchedules = _fixture.Build<VestedShedule>().CreateMany(1);
        
        // Act
        var result = vestedSchedules.ToCSV(digits);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }
}
