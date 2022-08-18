
using FluentValidation;
using Application.Commons.Extensions;

namespace Application.Commons.Domain;

public class InputValidator : AbstractValidator<Input>
{
    public InputValidator()
    {
        RuleFor(input => input.FileName)
            .NotNull().NotEmpty().WithMessage("File name is null or empty");

        RuleFor(input => input)
            .Must(FileExists).WithMessage("File not exists");

        RuleFor(input => input.Digits)
            .InclusiveBetween(0, 6)
            .WithMessage("Digits should be between 0 and 6");
    }

    private bool FileExists(Input input) =>
        File.Exists(input.GetFilePath());

    public static ValidationResult Execute(Input input)
    {
        var result = new InputValidator().Validate(input);
        
        return result.ToDomainResult();
    }
}