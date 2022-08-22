namespace Application.Commons.Utils;

public static class FileUtil
{
    public static async Task<IEnumerable<T>> ReadAllLines<T>(string fileName, CancellationToken cancellationToken, 
        Func<string[], T> parseValuesFunc, 
        Action<int, Exception> exceptionHandler)
    {
        var reader = new StreamReader(File.OpenRead(fileName));
        var result = new List<T>();
        var lineNumber = 0;
        
        while (!reader.EndOfStream)
        try
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            lineNumber ++;
            var line = await reader.ReadLineAsync();
            
            if (string.IsNullOrEmpty(line))
                continue;

            var lineValues = line.Split(",");
            var item = parseValuesFunc(lineValues);

            result.Add(item);
        }
        catch (Exception ex) 
        {
            exceptionHandler(lineNumber, ex);
        }
        return result;
    }
}
