using Vesting.Worker.Domain;

namespace Vesting.Worker;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var input = args.TryParseToInput();
            Console.WriteLine($"Starting with arguments {input.ToString()}");

            var validationResult = InputValidator.Execute(input);
            if (!validationResult.IsValid)
            {
                Console.WriteLine($"Invalid Input: {validationResult.Errors.ToString()}");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
