using System.Globalization;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Extensions;

public static class GetVestedExtensions
{
    public static GetVestedOutput ToOutput(this IEnumerable<VestedShedule> vestedShedules, int digits) =>
        new GetVestedOutput(vestedShedules, digits);

    public static IEnumerable<string> ToCSV(this IEnumerable<VestedShedule> vestedShedules, int digits)
    {
        return vestedShedules.Where(vested => vested is not null)
        .Select(vested => 
            $"{vested.EmployeeId}," + 
            $"{vested.EmployeeName}," +
            $"{vested.AwardId}," + 
            $"{vested.Quantity.ToString($"N{digits}", NumberFormatInfo)}");
    }

    private static NumberFormatInfo NumberFormatInfo =>
        new NumberFormatInfo { NumberGroupSeparator = string.Empty };
}
