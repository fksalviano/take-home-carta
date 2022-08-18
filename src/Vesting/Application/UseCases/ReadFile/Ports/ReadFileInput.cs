namespace Application.UseCases.ReadFile.Ports;

public class ReadFileInput
{
    public ReadFileInput(string fileName, int digits)
    {
        FileName = fileName;
        Digits = digits;
    }

    public string FileName { get; }
    public int Digits { get; }   
}