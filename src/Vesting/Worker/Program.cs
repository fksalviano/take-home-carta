
using Vesting.Application.Commons.Domain;
using Vesting.Application.Commons.Extensions;

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
                Console.WriteLine($"Invalid Input: {validationResult.Error}");
                return;
            }

            // TODO: chamar UseCases ReadFile e GetVested
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
        }
    }
}
