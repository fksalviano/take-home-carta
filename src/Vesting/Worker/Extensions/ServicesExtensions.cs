using Application.UseCases.GetVested;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Worker.Abstractions;
using Worker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Worker.Ports;

namespace Worker.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureServices(this IServiceCollection services) =>
        services
            .AddSingleton<IWorker, VestingWorker>()
            .AddSingleton<IWorkerOutputPort, WorkerOutputPort>()
            .AddSingleton<IReadFileUseCase, ReadFileUseCase>()
            .AddSingleton<IGetVestedUseCase, GetVestedUseCase>();

}
