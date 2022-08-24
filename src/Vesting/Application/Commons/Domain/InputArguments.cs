namespace Application.Commons.Domain;

public struct InputArguments
{
    public InputArguments(string fileName, DateTime targetDate, int digits = 0)
    {
        FileName = fileName;
        TargetDate = targetDate;
        Digits = digits;
    }

    public string FileName { get; }
    public DateTime TargetDate { get; }
    public int Digits { get; }
}
