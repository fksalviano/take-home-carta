using Application.Commons.Domain;
using Application.UseCases.ReadFile.Ports;

namespace Application.UseCases.ReadFile.Extensions;

public static class ReadFileExtensions
{
    public static ReadFileOutput ToOutput(this IEnumerable<VestingEvent> vestingEvents) =>
        new ReadFileOutput(vestingEvents);
}
