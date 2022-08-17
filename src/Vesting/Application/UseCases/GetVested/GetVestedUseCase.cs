using System.Text.RegularExpressions;
using Application.Commons.Domain;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVestedByAward.Abstractions;

namespace Application.UseCases.GetVested;

public class GetVestedUseCase : IGetVestedUseCase
{
    private IGetVestedOutputPort? _outputPort;

    public void SetOutputPort(IGetVestedOutputPort outputPort) =>
        _outputPort = outputPort;

    public async Task Execute(GetVestedInput input)
    {
        var vestingEvents = input.VestingEvents
            .Where(vesting => vesting.Date <= input.Date)
            .ToList();  

        var vestedSchedules = vestingEvents
            .GroupBy(vesting => new 
            { 
                vesting.EmployeeId, 
                vesting.EmployeeName,
                vesting.AwardId
            })
            .Select(group => new VestedShedule
            {
                EmployeeId = group.Key.EmployeeId,
                EmployeeName = group.Key.EmployeeName,
                AwardId = group.Key.AwardId,
                Quantity = SumQuantity(VestingType.VEST, group.ToList()) - SumQuantity(VestingType.CANCEL, group.ToList())
            });

        var output = vestedSchedules.ToOutput();
        await Task.Run(() => _outputPort!.Ok(output));
    }

    private decimal SumQuantity(VestingType type, IEnumerable<VestingEvent> vestingEvents) =>
        vestingEvents
            .Where(vesting => vesting.Type == type)
            .Sum(vesting => vesting.Quantity);
}
