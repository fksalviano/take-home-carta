using Application.Commons.Domain;
using Application.Commons.Extensions;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVestedByAward.Abstractions;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;

namespace Worker.Workers;

public class VestingWorker : IWorker, IGetVestedOutputPort 
{
    private readonly IReadFileUseCase _readFileUseCase;
    private readonly IGetVestedUseCase _getVestedUseCase;

    public VestingWorker(IReadFileUseCase readFileUseCase, IGetVestedUseCase getVestedUseCase)
    {
        _readFileUseCase = readFileUseCase;

        _getVestedUseCase = getVestedUseCase;
        _getVestedUseCase.SetOutputPort(this);
    }

    public async Task Execute(string[] args, CancellationToken cancellationToken)
    {
        if (ShowHelp(args))
            return;

        var input = args.TryParseToInput();
        if (!IsValidInput(input))
            return;

        var readFileInput = input.ToReadFileInput();
        var fileOutput = await _readFileUseCase.Execute(readFileInput, cancellationToken);

        var getVestedInput = input.ToGetVestedInput(fileOutput);
        await _getVestedUseCase.Execute(getVestedInput, cancellationToken);
    }

    private bool IsValidInput(Input input)
    {
        var result = InputValidator.Execute(input);

        if (!result.IsValid)
            Console.WriteLine($"Invalid Input: {result.Error}");
        
        return result.IsValid;
    }

    void IGetVestedOutputPort.Ok(GetVestedOutput output)
    {
        foreach (var line in output.VestedShedules.ToCSV(output.Digits))
            Console.WriteLine(line);
    }

    void IGetVestedOutputPort.NotFound() =>
        Console.WriteLine("NOT FOUND: Vesting events not found on file for this date");

    private bool ShowHelp(string[] args)
    {
        const string helpArg = "help";

        if (args.Any() && args[0] == helpArg)
        {
            var help = "\n" + 
                "Usage: vesting_program <File Name> <Target Date> <Precision Digits> \n\n" +
                "File Name: CSV Vesting Events file to read \n" +
                "Target Date: Date to filter events on or before it \n" +
                "Precision Digits: [Optional, default = 0] The number of decimals to round quantity values."; 

            Console.WriteLine(help);
            return true;
        }
        else
            return false;
    }
}
