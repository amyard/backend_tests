using SplitLargeFile;

string filePath = @"D:\rider_projects\backend_tests\SplitLargeFile\data\data.txt";

var unsorted = File.OpenRead(filePath);

Processing process = new Processing();
await process.SplitData(unsorted, "txt", CancellationToken.None);



