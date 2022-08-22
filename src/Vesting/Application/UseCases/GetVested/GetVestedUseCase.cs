using Application.Commons.Domain;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Domain;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using static Application.Commons.Domain.VestingType;

namespace Application.UseCases.GetVested;

public class GetVestedUseCase : IGetVestedUseCase
{
    private IGetVestedOutputPort? _outputPort;

    public void SetOutputPort(IGetVestedOutputPort outputPort) =>
        _outputPort = outputPort;

    public async Task ExecuteAsync(GetVestedInput input, CancellationToken cancellationToken)
    {
        var vestedSchedules = await Task.Run(() => 
            input.VestingEvents
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
                    Quantity = SumQuantity(group.ToList(), input.Date)  
                })
        );

        if (!vestedSchedules.Any())
        {
            _outputPort!.NotFound();
            return;
        }

        var output = vestedSchedules.ToOutput(input.Digits); 
        _outputPort!.Ok(output);
    }

    private decimal SumQuantity(IEnumerable<VestingEvent> vestingEvents, DateTime date)
    {
        var eventsByDate = vestingEvents.Where(vesting => vesting.Date <= date);
        
        return SumByType(VEST, eventsByDate) - SumByType(CANCEL, eventsByDate);
    }

    private decimal SumByType(VestingType type, IEnumerable<VestingEvent> vestingEvents) =>
        vestingEvents
            .Where(vesting => vesting.Type == type)
            .Sum(vesting => vesting.Quantity);
}