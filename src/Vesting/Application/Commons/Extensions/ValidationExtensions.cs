using Application.Commons.Domain;
using FluentValidationResult = FluentValidation.Results.ValidationResult;

namespace Application.Commons.Extensions;

public static class ValidationExtensions
{

    public static ValidationResult ToDomainResult(this FluentValidationResult result) =>
        new ValidationResult(result.IsValid, 
            string.Join(", ", result.Errors.Select(error => error.ErrorMessage)));
}
