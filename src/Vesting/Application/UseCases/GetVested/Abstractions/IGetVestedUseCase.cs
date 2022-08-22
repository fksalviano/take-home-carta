using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Abstractions;

public interface IGetVestedUseCase
{
    Task ExecuteAsync(GetVestedInput input, CancellationToken cancellationToken);

    void SetOutputPort(IGetVestedOutputPort outputPort);
}