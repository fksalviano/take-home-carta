using Application.Commons.Domain;
using Application.Commons.Utils;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Extensions;
using Application.UseCases.ReadFile.Ports;
using static System.Globalization.NumberStyles;
using static System.Globalization.CultureInfo;

namespace Application.UseCases.ReadFile;

public class ReadFileUseCase : IReadFileUseCase
{
    public async Task<ReadFileOutput> Execute(ReadFileInput input, CancellationToken cancellationToken)
    {
        var vestingEvents = await FileUtil.ReadAllLines(input.FileName, cancellationToken, 
            (lineValues) => new VestingEvent
            {
                Type = Enum.Parse<VestingType>(lineValues[0]),
                EmployeeId = lineValues[1],
                EmployeeName = lineValues[2], 
                AwardId = lineValues[3],
                Date = DateTime.Parse(lineValues[4]),
                Quantity = ParseQuantity(lineValues[5], input.Digits)
            },
            (lineNumber, ex) =>
                throw new InvalidDataException($"File Line {lineNumber} is invalid: {ex.Message}", ex)
        );

        return vestingEvents.ToOutput();
    }

    private decimal ParseQuantity(string quantity, int digits)
    {
        var parsed = decimal.Parse(quantity, Number, InvariantCulture);
        return Math.Round(parsed, digits, MidpointRounding.ToZero);
    }
}
