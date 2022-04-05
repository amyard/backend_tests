namespace SortingAlgorithm;

public class Process
{
    private long _maxUnsortedRows;
    private string[] _unsortedRows;
    private double _totalFilesToMerge;
    private int _mergeFilesProcessed;
    private readonly ExternalMergeSorterOptions _options;
    private const string UnsortedFileExtension = ".unsorted";
    private const string SortedFileExtension = ".sorted";
    private const string TempFileExtension = ".tmp";
    
    public Process() : this(new ExternalMergeSorterOptions()) { }

    private Process(ExternalMergeSorterOptions options)
    {
        _totalFilesToMerge = 0;
        _mergeFilesProcessed = 0;
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _unsortedRows = Array.Empty<string>();
    }

    /// <summary>
    /// Old version of sorting.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="token"></param>
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
    
    public async Task Sort(Stream source, Stream target, CancellationToken cancellationToken)
    {
        var files = await SplitFile(source, cancellationToken);
        var asd = "aaaa";
    }

    private async Task<IReadOnlyCollection<string>> SplitFile(Stream sourceStream, CancellationToken cancellationToken)
    {
        var fileSize = _options.Split.FileSize;
        var buffer = new byte[fileSize];
        var extraBuffer = new List<byte>();
        var filenames = new List<string>();

        await using (sourceStream)
        {
            var currentFile = 0L;
            while (sourceStream.Position < sourceStream.Length)
            {
                var totalRows = 0;
                var runBytesRead = 0;
                while (runBytesRead < fileSize)
                {
                    var value = sourceStream.ReadByte();
                    if (value == -1)
                    {
                        break;
                    }

                    var @byte = (byte)value;
                    buffer[runBytesRead] = @byte;
                    runBytesRead++;
                    if (@byte == _options.Split.NewLineSeparator)
                    {
                        // Count amount of rows, used for allocating a large enough array later on when sorting
                        totalRows++;
                    }
                }

                var extraByte = buffer[fileSize - 1];

                while (extraByte != _options.Split.NewLineSeparator)
                {
                    var flag = sourceStream.ReadByte();
                    if (flag == -1)
                    {
                        break;
                    }
                    extraByte = (byte)flag;
                    extraBuffer.Add(extraByte);
                }

                var filename = $"{++currentFile}.unsorted";
                await using var unsortedFile = File.Create(Path.Combine(_options.FileLocation, filename));
                await unsortedFile.WriteAsync(buffer, 0, runBytesRead, cancellationToken);
                if (extraBuffer.Count > 0)
                {
                    await unsortedFile.WriteAsync(extraBuffer.ToArray(), 0, extraBuffer.Count, cancellationToken);
                }

                if (totalRows > _maxUnsortedRows)
                {
                     // Used for allocating a large enough array later on when sorting.
                    _maxUnsortedRows = totalRows;
                }

                filenames.Add(filename);
                extraBuffer.Clear();
            }

            return filenames;
        }
    }
}