using System.Globalization;
using Application.Commons.Domain;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Extensions;

public static class GetVestedExtensions
{
    public static GetVestedOutput ToOutput(this IEnumerable<VestedShedule> vestedShedules) =>
        new GetVestedOutput(vestedShedules);

    public static IEnumerable<string?> ToCSV(this IEnumerable<VestedShedule> vestedShedules) =>
        vestedShedules.Select(vested => 
            $"{vested.EmployeeId},{vested.EmployeeName},{vested.AwardId},{vested.Quantity}");
}
