namespace Application.Commons.Domain;

public class ValidationResult
{
    public ValidationResult(bool isValid, string error)
    {
        IsValid = isValid;
        Error = error;
    }

    public bool IsValid { get; }
    public string Error { get; }
}