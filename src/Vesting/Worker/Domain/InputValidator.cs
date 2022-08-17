
using FluentValidation;
using FluentValidation.Results;

namespace Vesting.Worker.Domain
{
    public class InputValidator : AbstractValidator<Input>
    {
        public InputValidator()
        {
            RuleFor(input => input.FileName)
                .Must(FileExists)
                .WithMessage("File not exists");

            RuleFor(input => input.Digits)
                .InclusiveBetween(0, 6)
                .WithMessage("Digits should be between 0 and 6");
        }

        private bool FileExists(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.Exists(filePath);
        }

        public static ValidationResult Execute(Input input) =>
            new InputValidator().Validate(input);
    }
}