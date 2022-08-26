using Application.UseCases.GetVested;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;
using Worker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Worker.Ports;
using System.Diagnostics.CodeAnalysis;

namespace Worker.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void ConfigureServices(this IServiceCollection services) =>
        services
            .AddSingleton<IWorker, VestingWorker>()
            .AddSingleton<IWorkerOutputPort, WorkerOutputPort>()

            .AddSingletonWithValidation<IReadFileUseCase, ReadFileUseCase>(useCase =>
                new ReadFileUseCaseValidation(useCase))

            .AddSingletonWithValidation<IGetVestedUseCase, GetVestedUseCase>(useCase => 
                new GetVestedUseCaseValidation(useCase));

    public static IServiceCollection AddSingletonWithValidation<TInterface, TUseCase>(
        this IServiceCollection services, Func<TUseCase, TInterface> getValidationFunc)
        where TInterface: class where TUseCase: class, TInterface 
        {
            services.AddSingleton<TUseCase>();            
            return services.AddSingleton<TInterface>(provider => 
                getValidationFunc(provider.GetRequiredService<TUseCase>()));
        }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;
}
