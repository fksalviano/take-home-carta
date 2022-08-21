namespace Worker.Abstractions;

public interface IWorkerOutputPort
{
    void Ok(IEnumerable<string> output);
    void Invalid(string error);
    void NotFound();
}
