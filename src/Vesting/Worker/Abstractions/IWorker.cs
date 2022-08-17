namespace Worker.Abstractions;

public interface IWorker
{
    Task Execute(string[] args);
}
