using Application.Commons.Domain.Validators;

namespace Application.Commons.Extensions;

public static class ValidationExtensions
{

    public static ValidationResult ToDomainResult(this FluentValidation.Results.ValidationResult result) =>
        new ValidationResult(result.IsValid, 
            string.Join(", ", result.Errors.Select(error => error.ErrorMessage)));
}
