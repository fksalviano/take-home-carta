using Application.Commons.Domain;
using FluentValidation.Results;

namespace Application.Commons.Extensions;

public static class ResultExtensions
{

    public static Result ToDomainResult(this ValidationResult validation) =>
        new Result(validation.IsValid, 
            string.Join(", ", validation.Errors.Select(error => error.ErrorMessage)));
}
