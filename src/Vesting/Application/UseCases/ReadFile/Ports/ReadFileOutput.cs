using Application.Commons.Domain;

namespace Application.UseCases.ReadFile.Ports;

public struct ReadFileOutput
{
    public ReadFileOutput(IEnumerable<VestingEvent> vestingEvents)
    {
        VestingEvents = vestingEvents;
    }

    public IEnumerable<VestingEvent> VestingEvents { get; }
}
