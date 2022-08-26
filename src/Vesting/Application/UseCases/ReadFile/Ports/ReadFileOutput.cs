using System.Collections;
using Application.Commons.Domain;

namespace Application.UseCases.ReadFile.Ports;

public struct ReadFileOutput : IEnumerable<VestingEvent>
{
    private IEnumerable<VestingEvent> _vestingEvents;

    public ReadFileOutput(IEnumerable<VestingEvent> vestingEvents) =>
        _vestingEvents = vestingEvents;

    public ReadFileOutput(ValidationResult error)
    {
        Validation = error;
        _vestingEvents = new List<VestingEvent>();
    }

    public IEnumerator<VestingEvent> GetEnumerator() =>
        _vestingEvents.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        this.GetEnumerator();

    public bool IsValid => Validation.IsValid;

    public ValidationResult Validation { get; } = new ValidationResult(true, string.Empty);
}
