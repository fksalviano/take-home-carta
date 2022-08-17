using FluentValidation.Results;

namespace Vesting.Worker.Domain;

public static class WorkerExtensions
{
    public static Input TryParseToInput(this string[] args)
    {
        if (args.Count() == 0 || string.IsNullOrEmpty(args[0]))
            throw new ArgumentException("Please inform the File Name", nameof(Input.FileName));
        var fileName = args[0];

        if (args.Count() == 1 || string.IsNullOrEmpty(args[1]))
            throw new ArgumentException("Please inform the the Target Date", nameof(Input.TargetDate));

        if (!DateTime.TryParse(args[1], out var targetDate))
            throw new ArgumentException("Invalid argument Target Date", nameof(Input.TargetDate));

        var digits = 0;
        if (args.Count() == 3)
        {
            if (!int.TryParse(args[2], out digits))
                throw new ArgumentException("Invalid argument Digits", nameof(Input.Digits));
        }

        return new Input(fileName, targetDate, digits);
    }

    public static string ToString(this IEnumerable<ValidationFailure> errors) =>
        string.Join(", ", errors.Select(error => error.ErrorMessage));
}
