namespace Application.Commons.Utils;

public static class FileReader
{
    public static async IAsyncEnumerable<T> GetContent<T>(string filePath, 
        Func<string[], T> parseValuesFunc,
        Action<int, Exception> exceptionHandler)
    {        
        var reader = new StreamReader(File.OpenRead(filePath));
        var lineNumber = 0;

        while (!reader.EndOfStream)
        {
            lineNumber ++;
            var line = await reader.ReadLineAsync();

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
                continue;
            }
            yield return item;
        }
    }
}