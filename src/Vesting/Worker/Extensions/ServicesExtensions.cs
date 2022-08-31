using Worker.Abstractions;
using Worker.Workers;
using Microsoft.Extensions.DependencyInjection;
using Worker.Ports;
using System.Diagnostics.CodeAnalysis;
using Application.Commons.Extensions;

namespace Worker.Extensions;

[ExcludeFromCodeCoverage]
public static class ServicesExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IWorker, VestingWorker>()
            .AddSingleton<IWorkerOutputPort, WorkerOutputPort>()
            .InstallServices();       
    }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;
}
