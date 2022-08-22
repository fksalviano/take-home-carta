namespace Application.Commons.Domain.Validators;

public struct ValidationResult
{
    public ValidationResult(bool isValid, string error)
    {
        IsValid = isValid;
        Error = error;
    }

    public bool IsValid { get; }
    public string Error { get; }
}