using Application.UseCases.GetVested.Domain;

namespace Application.UseCases.GetVested.Ports;

public struct GetVestedOutput
{
    public GetVestedOutput(IEnumerable<VestedSchedule> vestedSchedules, int digits)
    {
        VestedSchedules = vestedSchedules;
        Digits = digits;
    }

    public IEnumerable<VestedSchedule> VestedSchedules { get; }
    public int Digits { get; }
}
