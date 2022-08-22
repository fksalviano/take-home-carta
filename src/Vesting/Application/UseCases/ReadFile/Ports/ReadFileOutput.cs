using System.Collections;
using Application.Commons.Domain;

namespace Application.UseCases.ReadFile.Ports;

public struct ReadFileOutput : IEnumerable<VestingEvent>
{
    public ReadFileOutput(IEnumerable<VestingEvent> vestingEvents) =>
        _vestingEvents = vestingEvents;

    private IEnumerable<VestingEvent> _vestingEvents;

    public IEnumerator<VestingEvent> GetEnumerator() =>
        _vestingEvents.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();
}
