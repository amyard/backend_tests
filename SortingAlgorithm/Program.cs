using System.Diagnostics;
using Process = SortingAlgorithm.Process;

var processing = new Process();

// var unsorted = File.OpenRead(@"D:\rider_projects\test_api\generated_data\sorttest\date_1000.csv");
// var sorted = new FileStream(@"D:\rider_projects\test_api\generated_data\sorttest\Sorted.csv", FileMode.OpenOrCreate, FileAccess.Write);
//
//
// Console.WriteLine("1. Sorting process start.");
// var timer = new Stopwatch();
// timer.Start();
//
// await processing.Execute(unsorted, sorted, new CancellationToken()); 
//
// timer.Stop();
// TimeSpan timeTaken = timer.Elapsed;
// Console.WriteLine($"Sorting process finish. Time {timeTaken}");
    

var unsorted2 = File.OpenRead(@"D:\rider_projects\test_api\generated_data\Date_04_04_2022_10_03-iteration_250000.csv");
var sorted2 = new FileStream(@"D:\rider_projects\test_api\generated_data\sorttest\Sorted2.csv", FileMode.OpenOrCreate, FileAccess.Write);

Console.WriteLine("2. Sorting process start.");
var timer2 = new Stopwatch();
timer2.Start();

await processing.Sort(unsorted2, sorted2, CancellationToken.None); 

timer2.Stop();
TimeSpan timeTaken2 = timer2.Elapsed;
Console.WriteLine($"Sorting process finish. Time {timeTaken2}");


