using System.Globalization;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Extensions;

public static class GetVestedExtensions
{
    public static GetVestedInput ToInput(this string[] args)
    {
        string? fileName = null;
        DateTime? date = null;
        int digits = 0;

        if (args.Count() >= 1 && !string.IsNullOrEmpty(args[0]))
            fileName = args[0];

        if (args.Count() >= 2 && !string.IsNullOrEmpty(args[1]))
            if (DateTime.TryParse(args[1], out var parsedDate))
                date = parsedDate;

        if (args.Count() >=3)
            if (int.TryParse(args[2], out var parsedDigits))
                digits = parsedDigits;

        return new GetVestedInput(fileName, date, digits);
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
        Path.Combine(Directory.GetCurrentDirectory(), input.FileName!);
}
