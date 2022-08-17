namespace Vesting.Worker.Domain;

public class Input
{
    public Input(string fileName, DateTime targetDate, int digits = 0)
    {
        FileName = fileName;
        TargetDate = targetDate;
        Digits = digits;
    }

    public string FileName { get; }
    public DateTime TargetDate { get; }
    public int Digits { get; }

    public override string ToString() => string.Join(", ", 
        $"{nameof(FileName)}: {FileName}",
        $"{nameof(TargetDate)}: {TargetDate.ToShortDateString()}",
        $"{nameof(Digits)}: {Digits}");
}
