namespace Application.UseCases.ReadFile.Utils;

public static class FileReader
{
    public static IEnumerable<T> GetAsEnumerable<T>(string filePath, 
        Func<string[], T> parseValuesFunc,
        Action<int, Exception> exceptionHandler)
    {
        var reader = new StreamReader(File.OpenRead(filePath));
        var lineNumber = 0;

        while (!reader.EndOfStream)
        {
            lineNumber ++;
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            T item;
            try
            {
                var lineValues = line.Split(",");
                item = parseValuesFunc(lineValues);
            }
            catch (Exception ex)
            {
                exceptionHandler(lineNumber, ex);
                break;
            }
            yield return item;
        }
    }
}