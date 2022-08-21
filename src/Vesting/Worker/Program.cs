using Microsoft.Extensions.DependencyInjection;
using Worker.Abstractions;
using Worker.Extensions;

namespace Worker;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
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
        var cancellationSource = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("Cancelling...");
            cancellationSource.Cancel();
            eventArgs.Cancel = true;
            Environment.Exit(-1);
        };

        return cancellationSource.Token;
    }
}
