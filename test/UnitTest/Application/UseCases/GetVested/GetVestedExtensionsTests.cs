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

    [Theory]
    [MemberData(nameof(ValidArgumentsData))]
    public void ShouldConvertToInputSuccessfully(string[] args)
    {
        // Act
        var result = args.ToInput();

        // Assert
        result.Should().NotBeNull();
    }

    public static object[] ValidArgumentsData() => new object[] 
    {
        new object[] { new string[] {"example.csv", "2020-03-03"} },
        new object[] { new string[] {"example.csv", "2020-03-03", "1"} }
    };

    [Theory]
    [MemberData(nameof(InvalidArgumentsData))]
    public void ShouldConvertToInputInvalid(string[] args)
    {
        // Act
        var input = args.ToInput(); 

        // Assert
        input.Should().NotBeNull();
        input.Should().Match<GetVestedInput>(input => 
            input.FileName == null ||
            input.Date == null ||
            input.Digits == null);
    }

    public static object[] InvalidArgumentsData() => new object[]
    {
        new object[] { new string[] {} }, //all null
        new object[] {new string[] {"example.csv"} }, //null date
        new object[] {new string[] {"example.csv", "XXXX-XX-XX"} }, //invalid date
        new object[] {new string[] {"example.csv", "2020-03-03", "X"} } //invalid digit
    };
}
