using Application.Commons.Domain;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;

namespace Application.UseCases.GetVestedByAward.Abstractions;

public interface IGetVestedUseCase
{
    Task Execute(GetVestedInput input);

    void SetOutputPort(IGetVestedOutputPort outputPort);
}