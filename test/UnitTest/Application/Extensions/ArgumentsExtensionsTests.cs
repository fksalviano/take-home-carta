using FluentAssertions;
using Application.Commons.Extensions;

namespace UnitTest.Application.Extensions;

public class WorkerExtensionsTests
{
    [Theory]
    [MemberData(nameof(ValidArgumentsData))]
    public void ShouldTryParseToInputSuccess(string[] args)
    {
        // Act
        var result = args.TryParseToInput();

        // Assert
        result.Should().NotBeNull();
    }

    public static object[] ValidArgumentsData() => new object[] 
    {
        new object[] { new string[] {"example.csv", "2020-03-03"} },
        new object[] { new string[] {"example.csv", "2020-03-03", "1"} }
    };

    [Theory]
    [MemberData(nameof(InvalidArgumensData))]
    public void ShouldTryParseToInputFail(string[] args)
    {
        // Act
        var action = () => args.TryParseToInput(); 

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    public static object[] InvalidArgumensData() => new object[]
    {
        new object[] { new string[] {} }, //all null
        new object[] {new string[] {"example.csv"} }, //null date
        new object[] {new string[] {"example.csv", "XXXX-XX-XX"} }, //invalid date
        new object[] {new string[] {"example.csv", "2020-03-03", "X"} } //invalid digit
    };
}