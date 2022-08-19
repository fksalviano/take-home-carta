using Application.UseCases.GetVested.Domain;

namespace Application.UseCases.GetVested.Ports;

public struct GetVestedOutput
{
    public GetVestedOutput(IEnumerable<VestedShedule> vestedShedules, int digits)
    {
        VestedShedules = vestedShedules;
        Digits = digits;
    }

    public IEnumerable<VestedShedule> VestedShedules { get; }
    public int Digits { get; }
}
