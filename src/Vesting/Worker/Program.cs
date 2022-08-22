using Worker.Abstractions;
using Worker.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Worker;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();

        var worker = services.GetService<IWorker>();
        try
        {
            var cancellationSource = new CancellationTokenSource();
            cancellationSource.ConfigureCancelEvent(ConsoleCancel);

            await worker.ExecuteAsync(args, cancellationSource.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    private static void ConsoleCancel()
    {
        Console.WriteLine("Cancelling...");
        Environment.Exit(-1);
    }
}
