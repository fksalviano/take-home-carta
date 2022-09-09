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
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.InstallServices();

        var useCase = services.BuildServiceProvider().GetService<IGetVestedUseCase>()!;
        useCase.SetOutputPort(OutputPort.Create());
        try
        {
            var cancellationSource = new CancellationTokenSource();
            cancellationSource.ConfigureCancelEvent(OnCancel);

            var input = args.TryParseToInput();
            await useCase.ExecuteAsync(input, cancellationSource.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            Environment.ExitCode = 1;
        }
    }

    private static void OnCancel()
    {
        Console.WriteLine("Cancelling...");
        Environment.Exit(-1);
    }
}

public class OutputPort : IGetVestedOutputPort
{
    public static OutputPort Create() => new OutputPort();

    public void Ok(GetVestedOutput output)
    {
        foreach (var line in output.ToCSV())
            Console.WriteLine(line);
    }

    public void Invalid(ValidationResult result) =>
        Console.WriteLine($"Invalid input: {result.Error}");

    public void NotFound() =>
       Console.WriteLine("NOT FOUND: Vesting events not found on file");
}