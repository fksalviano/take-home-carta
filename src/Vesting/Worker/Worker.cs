using Application.Commons.Domain;
using Application.Commons.Extensions;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVestedByAward.Abstractions;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;

namespace Worker;

public class Worker : IWorker, IGetVestedOutputPort 
{
    private readonly IReadFileUseCase _readFileUseCase;
    private readonly IGetVestedUseCase _getVestedUseCase;
    
    public Worker(IReadFileUseCase readFileUseCase, IGetVestedUseCase getVestedUseCase)
    {
        _readFileUseCase = readFileUseCase;

        _getVestedUseCase = getVestedUseCase;
        _getVestedUseCase.SetOutputPort(this);
    }

    public async Task Execute(string[] args)
    {
        var input = args.TryParseToInput();
        Console.WriteLine($"Starting with arguments {input.ToString()}");

        if (!IsValidInput(input))
            return;

        var readFileInput = input.ToReadFileInput();
        var fileOutput = await _readFileUseCase.Execute(readFileInput);

        var getVestedInput = input.ToGetVestedInput(fileOutput);
        await _getVestedUseCase.Execute(getVestedInput);
    }

    void IGetVestedOutputPort.NotFound()
    {
        Console.WriteLine("NOT FOUND: Vesting events not found on file for this date");
    }

    void IGetVestedOutputPort.Ok(GetVestedOutput output)
    {
        foreach (var line in output.VestedShedules.ToCSV())
            Console.WriteLine(line);
    }

    private bool IsValidInput(Input input)
    {
        var result = InputValidator.Execute(input);

        if (!result.IsValid)
            Console.WriteLine($"Invalid Input: {result.Error}");
        
        return result.IsValid;
    }
}
