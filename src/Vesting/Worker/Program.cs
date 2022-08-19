using Application.UseCases.GetVested;
using Application.UseCases.GetVestedByAward.Abstractions;
using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Worker.Abstractions;
using Worker.Workers;

namespace Worker;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        try
        {
            var cancellationToken = GetCancellationToken();

            await services.BuildServiceProvider()
                .GetService<IWorker>()!
                .Execute(args, cancellationToken);

            Environment.ExitCode = 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    private static CancellationToken GetCancellationToken()
    {
        var source = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("Cancelling...");
            source.Cancel();
            eventArgs.Cancel = true;
            Environment.Exit(-1);
        };

        return source.Token;
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IWorker, VestingWorker>()
            .AddSingleton<IReadFileUseCase, ReadFileUseCase>()
            .AddSingleton<IGetVestedUseCase, GetVestedUseCase>();
    }
}
