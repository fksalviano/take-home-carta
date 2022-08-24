using Application.Commons.Domain;

namespace Application.Commons.Extensions;

public static class WorkerExtensions
{
    public static InputArguments TryParseToInput(this string[] args)
    {
        if (args.Count() == 0 || string.IsNullOrEmpty(args[0]))
            throw new ArgumentException("Please inform the File Name", nameof(InputArguments.FileName));
        var fileName = args[0];

        if (args.Count() == 1 || string.IsNullOrEmpty(args[1]))
            throw new ArgumentException("Please inform the the Target Date", 
                nameof(InputArguments.TargetDate));

        if (!DateTime.TryParse(args[1], out var targetDate))
            throw new ArgumentException($"Invalid argument Target Date: {args[1]} is not a valid date", 
                nameof(InputArguments.TargetDate));

        var digits = 0;
        if (args.Count() == 3)
        {
            if (!int.TryParse(args[2], out digits))
                throw new ArgumentException("Invalid argument Digits", nameof(InputArguments.Digits));
        }

        return new InputArguments(fileName, targetDate, digits);
    }
}
