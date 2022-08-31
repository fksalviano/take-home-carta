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
    public static void ConfigureServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IWorker, VestingWorker>()
            .AddSingleton<IWorkerOutputPort, WorkerOutputPort>()

            .AddSingletonWithValidation<IReadFileUseCase, ReadFileUseCase>(useCase =>
                new ReadFileUseCaseValidation(useCase))

            .AddSingletonWithValidation<IGetVestedUseCase, GetVestedUseCase>(useCase => 
                new GetVestedUseCaseValidation(useCase));
    }

    public static IServiceCollection AddSingletonWithValidation<TInterface, TUseCase>(
        this IServiceCollection services, Func<TUseCase, TInterface> getValidationInstance)
        where TInterface: class where TUseCase: class, TInterface 
        {
            services.AddSingleton<TUseCase>();
            services.AddSingleton<TInterface>(provider =>
            {
                var instance = provider.GetRequiredService<TUseCase>();
                return getValidationInstance(instance);
            });
            return services;
        }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;
}
