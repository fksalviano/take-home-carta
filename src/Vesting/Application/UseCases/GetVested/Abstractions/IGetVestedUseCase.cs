using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVested.Abstractions;

public interface IGetVestedUseCase
{
    Task Execute(GetVestedInput input, CancellationToken cancellationToken);

    void SetOutputPort(IGetVestedOutputPort outputPort);
}