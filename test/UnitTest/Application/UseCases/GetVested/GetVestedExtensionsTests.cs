using Application.Commons.Domain;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
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
        var vestedSchedules = _fixture.Build<VestedSchedule>().CreateMany(1);
        
        // Act
        var result = vestedSchedules.ToOutput(digits);

        // Assert
        result.Should().NotBeNull();
        result.VestedSchedules.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldConvertToCSV()
    {
        // Arrange
        var digits = 1;
        var output = new GetVestedOutput(_fixture.Build<VestedSchedule>().CreateMany(1), digits);
        
        // Act
        var result = output.ToCSV();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }
}
