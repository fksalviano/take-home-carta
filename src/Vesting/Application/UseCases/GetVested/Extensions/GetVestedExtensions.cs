using System.Globalization;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Extensions;

public static class GetVestedExtensions
{
    public static GetVestedInput TryParseToInput(this string[] args)
    {
        if (args.Count() == 0 || string.IsNullOrEmpty(args[0]))
            throw new ArgumentException("Please inform the File Name", nameof(GetVestedInput.FileName));
        var fileName = args[0];

        if (args.Count() == 1 || string.IsNullOrEmpty(args[1]))
            throw new ArgumentException("Please inform the the Target Date", 
                nameof(GetVestedInput.Date));

        if (!DateTime.TryParse(args[1], out var targetDate))
            throw new ArgumentException($"Invalid argument Target Date: {args[1]} is not a valid date", 
                nameof(GetVestedInput.Date));

        var digits = 0;
        if (args.Count() == 3)
        {
            if (!int.TryParse(args[2], out digits))
                throw new ArgumentException("Invalid argument Digits", nameof(GetVestedInput.Digits));
        }

        return new GetVestedInput(fileName, targetDate, digits);
    }

    public static GetVestedOutput ToOutput(this IEnumerable<VestedSchedule> vestedSchedules, int digits) =>
        new GetVestedOutput(vestedSchedules, digits);

    public static IEnumerable<string> ToCSV(this GetVestedOutput output) =>
        output.VestedSchedules
            .Select(vested =>
                $"{vested.EmployeeId}," +
                $"{vested.EmployeeName}," +
                $"{vested.AwardId}," +
                $"{vested.Quantity.ToString($"N{output.Digits}", NumberFormatInfo)}");

    private static NumberFormatInfo NumberFormatInfo =>
        new NumberFormatInfo { NumberGroupSeparator = string.Empty };

    public static string GetFilePath(this GetVestedInput input) =>
        Path.Combine(Directory.GetCurrentDirectory(), input.FileName);
}
