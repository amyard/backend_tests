namespace SplitLargeFile;

public class Processing
{
    private int fileSize = 1024 * 3;
    private char newLineSeparator = '\n';
    private readonly string _folderSplittedPath = @"D:\rider_projects\backend_tests\SplitLargeFile\splitedData";
    
    public async Task SplitData(Stream sourceStream, string folderName, CancellationToken cancellationToken)
    {
        var files = await SplitDataProcess(sourceStream, folderName, cancellationToken);
    }
    
    public async Task<IReadOnlyCollection<string>> SplitDataProcess(Stream sourceStream, string folderName, CancellationToken cancellationToken)
    {
        var buffer = new byte[fileSize];
        var extraBuffer = new List<byte>();
        var filenames = new List<string>();
        
        await using (sourceStream)
        {
            var currentFile = 0L;
            while (sourceStream.Position < sourceStream.Length)
            {
                var runBytesRead = 0;
                while (runBytesRead < fileSize)
                {
                    var value = sourceStream.ReadByte();
                    if (value == -1)
                    {
                        break;
                    }

                    var @byte = (byte) value;
                    buffer[runBytesRead] = @byte;
                    runBytesRead++;
                }

                var extraByte = buffer[fileSize - 1];

                while (extraByte != newLineSeparator)
                {
                    var flag = sourceStream.ReadByte();
                    if (flag == -1)
                    {
                        break;
                    }

                    extraByte = (byte) flag;
                    
                    if (extraByte != (byte)newLineSeparator)
                        extraBuffer.Add(extraByte);
                }

                var filename = $"{++currentFile}.unsorted";
                await using var unsortedFile = File.Create(Path.Combine(_folderSplittedPath, folderName, filename));
                await unsortedFile.WriteAsync(buffer, 0, runBytesRead, cancellationToken);
                if (extraBuffer.Count > 0)
                {
                    await unsortedFile.WriteAsync(extraBuffer.ToArray(), 0, extraBuffer.Count-1, cancellationToken);
                }

                filenames.Add(filename);
                extraBuffer.Clear();
            }

            return filenames;
        }
    }
}