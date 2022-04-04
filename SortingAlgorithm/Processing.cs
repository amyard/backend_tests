namespace SortingAlgorithm;

public class Processing
{
    public async Task Execute(Stream source, Stream target, CancellationToken token)
    {
        using var streamReader = new StreamReader(source);
        var lines = new List<string>();
        while (!streamReader.EndOfStream)
        {
            lines.Add(await streamReader.ReadLineAsync());
        }

        await using var streamWriter = new StreamWriter(target);
        foreach (var line in lines.OrderBy(x => x))
        {
            await streamWriter.WriteLineAsync(line);
        }
    }
}