namespace Application.Commons.Utils;

public static class FileUtil
{
    public static async Task<IEnumerable<T>> ReadAllLines<T>(string fileName, CancellationToken cancellationToken, 
        Func<string[], int, T> parseValuesFunc)
    {
        var reader = new StreamReader(File.OpenRead(fileName));
        var result = new List<T>();
        var lineNumber = 0;
        
        while (!reader.EndOfStream)
        {
            lineNumber ++;
            var line = await reader.ReadLineAsync();
            
            if (string.IsNullOrEmpty(line))
                continue;

            var lineValues = line.Split(",");
            var item = parseValuesFunc(lineValues, lineNumber);

            result.Add(item);
        }
        return result;
    }
}
