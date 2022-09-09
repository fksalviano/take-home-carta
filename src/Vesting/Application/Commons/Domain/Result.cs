namespace Application.Commons.Domain;

public struct Result
{
    public Result(bool isValid, string error = "")
    {
        IsValid = isValid;
        ErrorMessage = error;
    }

    public bool IsValid { get; }
    public string ErrorMessage { get; }

    public static Result Error(string error) => new Result(false, error);
}