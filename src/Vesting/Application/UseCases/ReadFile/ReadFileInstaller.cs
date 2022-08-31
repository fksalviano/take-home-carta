using System.Diagnostics.CodeAnalysis;
using Application.Commons.Abstractions;
using Application.Commons.Extensions;
using Application.UseCases.ReadFile.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.ReadFile;

[ExcludeFromCodeCoverage]
public class ReadFileInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services)
    {
        services
            .AddSingletonWithValidation<IReadFileUseCase, ReadFileUseCase>(useCase =>
                new ReadFileUseCaseValidation(useCase));     
    }
}