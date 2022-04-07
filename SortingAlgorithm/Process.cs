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
        
        // Here we create a new array that will hold the unsorted rows used in SortFiles.
        // If we have more than 100 files, files will be unsorted.
        _unsortedRows = new string[_maxUnsortedRows];
        var sortedFiles = await SortFiles(files);

        
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
    
    private async Task<IReadOnlyList<string>> SortFiles(IReadOnlyCollection<string> unsortedFiles)
    {
        var sortedFiles = new List<string>(unsortedFiles.Count);
        double totalFiles = unsortedFiles.Count;
        foreach (var unsortedFile in unsortedFiles)
        {
            var sortedFilename = unsortedFile.Replace(UnsortedFileExtension, SortedFileExtension);
            var unsortedFilePath = Path.Combine(_options.FileLocation, unsortedFile);
            var sortedFilePath = Path.Combine(_options.FileLocation, sortedFilename);
            await SortFile(File.OpenRead(unsortedFilePath), File.OpenWrite(sortedFilePath));
            File.Delete(unsortedFilePath);
            sortedFiles.Add(sortedFilename);
        }
        return sortedFiles;
    }

    private async Task SortFile(Stream unsortedFile, Stream target)
    {
        using var streamReader = new StreamReader(unsortedFile, bufferSize: _options.Sort.InputBufferSize);
        var counter = 0;
        while (!streamReader.EndOfStream)
        {
            _unsortedRows[counter++] = (await streamReader.ReadLineAsync())!;
        }

        Array.Sort(_unsortedRows, _options.Sort.Comparer);
        await using var streamWriter = new StreamWriter(target, bufferSize: _options.Sort.OutputBufferSize);
        foreach (var row in _unsortedRows.Where(x => x != null))
        {
            await streamWriter.WriteLineAsync(row);
        }

        Array.Clear(_unsortedRows, 0, _unsortedRows.Length);
    }
}