namespace Worker.Abstractions;

public interface IWorker
{
    Task ExecuteAsync(string[] args, CancellationToken cancellationToken);
}
