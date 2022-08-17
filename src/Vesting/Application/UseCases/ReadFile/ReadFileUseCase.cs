using Application.Commons.Domain;
using Application.UseCases.ReadFile.Abstractions;
using Application.UseCases.ReadFile.Extensions;
using Application.UseCases.ReadFile.Ports;
using System.Globalization;

namespace Application.UseCases.ReadFile;

public class ReadFileUseCase : IReadFileUseCase
{
    public async Task<ReadFileOutput> Execute(ReadFileInput input)
    {
        var vestingEvents = new List<VestingEvent>();

        var lineNumber = 0;
        var reader = new StreamReader(File.OpenRead(input.FileName));

        while (!reader.EndOfStream)
        {
            lineNumber ++;
            var line = await reader.ReadLineAsync();
            
            if (!string.IsNullOrEmpty(line))
            {
                var values = line.Split(",");
                try
                {
                    var vestingEvent = new VestingEvent
                    {
                        Type = Enum.Parse<VestingType>(values[0]),
                        EmployeeId = values[1],
                        EmployeeName = values[2], 
                        AwardId = values[3],
                        Date = DateTime.Parse(values[4]),
                        Quantity = ParseQuantity(values[5], input.Digits) 
                    };
                    
                    vestingEvents.Add(vestingEvent);
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException($"ERROR READING FILE - Line {lineNumber}: {ex.Message}", ex);
                }
            }
        }

        return vestingEvents.ToOutput();
    }

    private decimal ParseQuantity(string quantity, int digits)
    {
        var parsed = decimal.Parse(quantity, NumberStyles.Number, CultureInfo.InvariantCulture);
        var result = Math.Round(parsed, digits, MidpointRounding.ToZero);

        return result;
    }
}
