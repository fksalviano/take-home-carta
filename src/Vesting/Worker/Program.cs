using Worker.Extensions;
using Application.Commons.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Application.UseCases.GetVested.Abstractions;
using Application.UseCases.GetVested.Ports;
using Application.UseCases.GetVested.Extensions;
using Application.Commons.Domain;

namespace Worker;

[ExcludeFromCodeCoverage]
class Program
{
    private static OutputPort _outputPort => new OutputPort();
    private static CancellationTokenSource _cts => new CancellationTokenSource()
        .ConfigureCancelEvent(OnCancel);

    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.InstallServices();

        var useCase = services.GetService<IGetVestedUseCase>()!;
        useCase.SetOutputPort(_outputPort);

        var input = args.ToInput();
        await useCase.ExecuteAsync(input, _cts.Token);
    }

    private static void OnCancel()
    {
        Console.WriteLine("Cancelling...");
        Environment.Exit(-1);
    }
}

public class OutputPort : IGetVestedOutputPort
{
    public void Ok(GetVestedOutput output)
    {
        foreach (var line in output.ToCSV())
            Console.WriteLine(line);
    }

    public void Invalid(Result result) =>
        Console.WriteLine($"Invalid input: {result.ErrorMessage}");

    public void NotFound() =>
       Console.WriteLine("NOT FOUND: Vesting events not found on file");
}