using Application.UseCases.GetVested;
using Application.UseCases.GetVestedByAward.Abstractions;
using Application.UseCases.ReadFile;
using Application.UseCases.ReadFile.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Worker.Abstractions;

namespace Worker;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        
        try
        {
            await services.BuildServiceProvider()
                .GetService<IWorker>()!
                .Execute(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IWorker, Worker>()
            .AddSingleton<IReadFileUseCase, ReadFileUseCase>()
            .AddSingleton<IGetVestedUseCase, GetVestedUseCase>();
    }
}
