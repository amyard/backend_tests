using System.Diagnostics;
using SortingAlgorithm;

var processing = new Processing();

var unsorted = File.OpenRead(@"D:\rider_projects\test_api\generated_data\sorttest\date_1000.csv");
var sorted = new FileStream(@"D:\rider_projects\test_api\generated_data\sorttest\Sorted.csv", FileMode.OpenOrCreate, FileAccess.Write);

Console.WriteLine("Sorting process start.");
var timer = new Stopwatch();
timer.Start();

await processing.Execute(unsorted, sorted, new CancellationToken()); 

timer.Stop();
TimeSpan timeTaken = timer.Elapsed;
Console.WriteLine($"Sorting process finish. Time {timeTaken}");
    
    
