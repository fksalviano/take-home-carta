namespace Worker.Extensions;

public static class CancellationTokenSourceExtensions
{
    public static void ConfigureCancelEvent(this CancellationTokenSource cancellationSource, Action onCancelAction)
    {
        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            cancellationSource.Cancel();
            eventArgs.Cancel = true;   
            onCancelAction();
        };
    }
}
