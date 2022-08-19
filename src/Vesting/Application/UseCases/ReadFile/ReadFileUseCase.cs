using Application.Commons.Domain;
using Application.Commons.Utils;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Extensions;
using Application.UseCases.ReadFile.Ports;
using System.Globalization;

namespace Application.UseCases.ReadFile;

public class ReadFileUseCase : IReadFileUseCase
{
    public async Task<ReadFileOutput> Execute(ReadFileInput input, CancellationToken cancellationToken)
    {
        var vestingEvents = await FileUtil
            .ReadAllLines(input.FileName, cancellationToken, (lineValues, lineNumber) => 
            {
                var vestingEvent = TryParseLine(lineValues, input.Digits, 
                    out var parseError);

                if (parseError is not null)
                    throw new InvalidDataException(
                        $"ERROR READING FILE - Line {lineNumber}: {parseError.Message}", parseError);

                return vestingEvent!;
            });

        return vestingEvents.ToOutput();
    }

    private VestingEvent? TryParseLine(string[] lineValues, int digits, out Exception? error)
    {
        error = null;
        try
        {
            return new VestingEvent
            {
                Type = Enum.Parse<VestingType>(lineValues[0]),
                EmployeeId = lineValues[1],
                EmployeeName = lineValues[2], 
                AwardId = lineValues[3],
                Date = DateTime.Parse(lineValues[4]),
                Quantity = ParseQuantity(lineValues[5], digits) 
            };
        }
        catch (Exception ex)
        {
            error = ex;
            return null;
        }
    }

    private decimal ParseQuantity(string quantity, int digits)
    {
        var parsed = decimal.Parse(quantity, NumberStyles.Number, CultureInfo.InvariantCulture);
        
        return Math.Round(parsed, digits, MidpointRounding.ToZero);
    }
}
