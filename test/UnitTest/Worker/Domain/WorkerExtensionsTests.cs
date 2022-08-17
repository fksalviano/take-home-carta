using Vesting.Worker.Domain;

namespace UnitTest.Worker.Domain;

public class WorkerExtensionsTests
{
    [Theory]
    [MemberData(nameof(ValidArgumentsData))]
    public void ShouldTryParseToInputSuccess(string[] args)
    {
        // Act
        var result = args.TryParseToInput();

        // Assert
        Assert.NotNull(result);
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
        // Assert
        Assert.Throws<ArgumentException>(() => args.TryParseToInput());
    }

    public static object[] InvalidArgumensData() => new object[]
    {
        new object[] { new string[] {} }, //all null
        new object[] {new string[] {"example.csv"} }, //null date
        new object[] {new string[] {"example.csv", "XXXX-XX-XX"} }, //invalid date
        new object[] {new string[] {"example.csv", "2020-03-03", "X"} } //invalid digit
    };
}