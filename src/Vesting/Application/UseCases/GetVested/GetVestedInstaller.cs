using System.Diagnostics.CodeAnalysis;
using Application.Commons.Abstractions;
using Application.Commons.Extensions;
using Application.UseCases.GetVested.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.GetVested;

[ExcludeFromCodeCoverage]
public class GetVestedInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services)
    {
        services
            .AddSingletonWithValidation<IGetVestedUseCase, GetVestedUseCase>(useCase =>
                new GetVestedUseCaseValidation(useCase));
    }
}