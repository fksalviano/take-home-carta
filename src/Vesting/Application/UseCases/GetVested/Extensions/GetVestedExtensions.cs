using System.Globalization;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Extensions;

public static class GetVestedExtensions
{
    public static GetVestedOutput ToOutput(this IEnumerable<VestedSchedule> vestedSchedules, int digits) =>
        new GetVestedOutput(vestedSchedules, digits);

    public static IEnumerable<string> ToCSV(this GetVestedOutput output)
    {
        return output.VestedSchedules.Where(vested => vested is not null)
        .Select(vested => 
            $"{vested.EmployeeId}," + 
            $"{vested.EmployeeName}," +
            $"{vested.AwardId}," + 
            $"{vested.Quantity.ToString($"N{output.Digits}", NumberFormatInfo)}");
    }

    private static NumberFormatInfo NumberFormatInfo =>
        new NumberFormatInfo { NumberGroupSeparator = string.Empty };
}
