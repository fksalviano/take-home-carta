using Application.Commons.Domain;

namespace Application.UseCases.GetVested.Ports
{
    public class GetVestedInput
    {
        public GetVestedInput(IEnumerable<VestingEvent> vestingEvents, DateTime date)
        {
            VestingEvents = vestingEvents;
            Date = date;
        }

        public DateTime Date { get; }

        public IEnumerable<VestingEvent> VestingEvents { get; }
    }
}