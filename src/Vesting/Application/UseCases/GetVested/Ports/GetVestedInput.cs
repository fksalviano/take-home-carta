using Application.Commons.Domain;

namespace Application.UseCases.GetVested.Ports;

public struct GetVestedInput
{
    public GetVestedInput(IEnumerable<VestingEvent> vestingEvents, DateTime date, int digits)
    {
        VestingEvents = vestingEvents;
        Date = date;
        Digits = digits;
    }

    public IEnumerable<VestingEvent> VestingEvents { get; }

    public DateTime Date { get; }
    public int Digits { get; }
}