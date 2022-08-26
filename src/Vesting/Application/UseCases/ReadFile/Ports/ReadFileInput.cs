namespace Application.UseCases.ReadFile.Ports;

public struct ReadFileInput
{
    public ReadFileInput(string fileName, int digits)
    {
        FilePath = GetFilePath(fileName);
        Digits = digits;
    }

    public string FilePath { get; }
    public int Digits { get; }   
    
    public static string GetFilePath(string fileName) =>
        Path.Combine(Directory.GetCurrentDirectory(), fileName);
}