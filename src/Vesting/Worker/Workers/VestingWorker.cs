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

    public VestingWorker(IReadFileUseCase readFileUseCase, IGetVestedUseCase getVestedUseCase, 
        IWorkerOutputPort outputPort)
    {
        _outputPort = outputPort;
        _readFileUseCase = readFileUseCase;

        _getVestedUseCase = getVestedUseCase;
        _getVestedUseCase.SetOutputPort(this);
    }

    public async Task ExecuteAsync(string[] args, CancellationToken cancellationToken)
    {
        var input = args.TryParseToInput();

        var readFileInput = input.ToReadFileInput();
        var fileOutput = await _readFileUseCase.ExecuteAsync(readFileInput, cancellationToken);

        if (!fileOutput.IsValid)
        {
            _outputPort.Invalid(fileOutput.Validation.Error);
            return;
        }

        var getVestedInput = input.ToGetVestedInput(fileOutput);
        await _getVestedUseCase.ExecuteAsync(getVestedInput, cancellationToken);
    }

    void IGetVestedOutputPort.Ok(GetVestedOutput output) =>
        _outputPort.Ok(output.ToCSV());

    void IGetVestedOutputPort.NotFound() =>
        _outputPort.NotFound();

    void IGetVestedOutputPort.Invalid(ValidationResult result) => 
        _outputPort.Invalid(result.Error);
}
