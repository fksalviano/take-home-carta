using Application.UseCases.GetVested.Domain;

namespace Application.UseCases.GetVested.Ports;

public class GetVestedOutput
{
    public GetVestedOutput(IEnumerable<VestedShedule> vestedShedules)
    {
        VestedShedules = vestedShedules;
    }

    public IEnumerable<VestedShedule> VestedShedules { get; }
}
