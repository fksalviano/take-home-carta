using Application.Commons.Domain;
using Application.Commons.Extensions;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Extensions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;

namespace Worker.Workers;

public class VestingWorker : IWorker, IGetVestedOutputPort 
{
    private readonly IReadFileUseCase _readFileUseCase;
    private readonly IGetVestedUseCase _getVestedUseCase;
    private readonly IWorkerOutputPort _outputPort;

    public VestingWorker(IReadFileUseCase readFileUseCase, IGetVestedUseCase getVestedUseCase, IWorkerOutputPort outputPort)
    {
        _outputPort = outputPort;
        _readFileUseCase = readFileUseCase;

        _getVestedUseCase = getVestedUseCase;
        _getVestedUseCase.SetOutputPort(this);
    }

    public async Task Execute(string[] args, CancellationToken cancellationToken)
    {
        var input = args.TryParseToInput();

        var validation = InputValidator.Execute(input);
        if (!validation.IsValid)
        {
            _outputPort.Invalid(validation.Error);
            return;
        }

        var readFileInput = input.ToReadFileInput();
        var fileOutput = await _readFileUseCase.Execute(readFileInput, cancellationToken);

        var getVestedInput = input.ToGetVestedInput(fileOutput);
        await _getVestedUseCase.Execute(getVestedInput, cancellationToken);
    }

    void IGetVestedOutputPort.Ok(GetVestedOutput output) =>
        _outputPort.Ok(output.VestedShedules
            .ToCSV(output.Digits));

    void IGetVestedOutputPort.NotFound() =>
        _outputPort.NotFound();
}
