using Application.UseCases.GetVested.Domain;
using Application.Commons.Utils;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using static Application.UseCases.GetVested.Domain.VestingType;
using static System.Globalization.NumberStyles;
using static System.Globalization.CultureInfo;
using Application.Commons.Domain;

namespace Application.UseCases.GetVested;

public class GetVestedUseCase : IGetVestedUseCase
{
    private IGetVestedOutputPort? _outputPort;

    public void SetOutputPort(IGetVestedOutputPort outputPort) =>
        _outputPort = outputPort;

    public async Task ExecuteAsync(GetVestedInput input, CancellationToken cancellationToken)
    {
        var filePath = input.GetFilePath();

        var vestingEvents = FileReader.GetContent(filePath,
            (lineValues) => new VestingEvent
            {
                Type = Enum.Parse<VestingType>(lineValues[0]),
                EmployeeId = lineValues[1],
                EmployeeName = lineValues[2],
                AwardId = lineValues[3],
                Date = DateTime.Parse(lineValues[4]),
                Quantity = Round(lineValues[5], input.Digits)
            },
            HandleException
        ).ToEnumerable();

        var schedules = await Task.Run(() => vestingEvents
            .GroupBy(vesting => new 
            { 
                vesting.EmployeeId, 
                vesting.EmployeeName, 
                vesting.AwardId 
            })
            .Select(group => new VestedSchedule
            {
                EmployeeId = group.Key.EmployeeId,
                EmployeeName = group.Key.EmployeeName,
                AwardId = group.Key.AwardId,
                Quantity = group
                    .Where(vesting => vesting.Date <= input.Date)
                    .Sum(vesting => vesting.Type == CANCEL 
                        ? vesting.Quantity * -1 
                        : vesting.Quantity)                
            }).ToList()
        );

        if (!schedules.Any())
        {
            _outputPort!.NotFound();
            return;
        }

        var output = schedules.ToOutput(input.Digits);
        _outputPort!.Ok(output);
    }

    private decimal Round(string quantity, int digits)
    {
        var parsed = decimal.Parse(quantity, Number, InvariantCulture);
        return Math.Round(parsed, digits, MidpointRounding.ToZero);
    }

    private void HandleException(int lineNumber, Exception ex) =>
        _outputPort!.Invalid(new ValidationResult(false,
            $"File Line {lineNumber} invalid: {ex.Message}"));
}