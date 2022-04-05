namespace SortingAlgorithm;

public class ExternalMergeSorterOptions
{
    public ExternalMergeSorterOptions()
    {
        Split = new ExternalMergeSortSplitOptions();
        Sort = new ExternalMergeSortSortOptions();
        Merge = new ExternalMergeSortMergeOptions();
    }

    public string FileLocation { get; init; } = "D:\\rider_projects\\test_api\\generated_data\\sortfull";
    public ExternalMergeSortSplitOptions Split { get; init; }
    public ExternalMergeSortSortOptions Sort { get; init; }
    public ExternalMergeSortMergeOptions Merge { get; init; }
}

public class ExternalMergeSortSplitOptions
{
    /// <summary>
    /// Size of unsorted file (chunk) in bytes
    /// </summary>
    public int FileSize { get; init; } = 2 * 1024 * 1024; // 2MByte file size.
    public char NewLineSeparator { get; init; } = '\n';
    public IProgress<double> ProgressHandler { get; init; }
}

public class ExternalMergeSortSortOptions
{
    public IComparer<string> Comparer { get; init; } = Comparer<string>.Default;
    public int InputBufferSize { get; init; } = 65536;
    public int OutputBufferSize { get; init; } = 65536;
    public IProgress<double> ProgressHandler { get; init; }
}

public class ExternalMergeSortMergeOptions
{
    /// <summary>
    /// How many files we will process per run
    /// </summary>
    public int FilesPerRun { get; init; } = 10;
    /// <summary>
    /// Buffer size (in bytes) for input StreamReaders
    /// </summary>
    public int InputBufferSize { get; init; } = 65536;
    /// <summary>
    /// Buffer size (in bytes) for output StreamWriter
    /// </summary>
    public int OutputBufferSize { get; init; } = 65536;
    public IProgress<double> ProgressHandler { get; init; }
}