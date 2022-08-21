using Worker.Abstractions;

namespace Worker.Ports;

public class WorkerOutputPort : IWorkerOutputPort
{
    public void Ok(IEnumerable<string> output) =>
        output.ToList().ForEach(line => 
            Console.WriteLine(line));

    public void Invalid(string error) =>
        Console.WriteLine($"Invalid input: {error}");

    public void NotFound() =>
       Console.WriteLine("NOT FOUND: Vesting events not found on file for this date");
    
}
