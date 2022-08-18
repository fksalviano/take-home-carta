using Application.Commons.Domain;

namespace Application.Commons.Extensions;

public static class ValidationExtensions
{

    public static ValidationResult ToDomainResult(this FluentValidation.Results.ValidationResult result) =>
        new ValidationResult(
            result.IsValid, 
            result.Errors.ToStrings());

    public static string ToStrings(this IEnumerable<FluentValidation.Results.ValidationFailure> errors) =>
        string.Join(", ", errors.Select(error => error.ErrorMessage));
}
