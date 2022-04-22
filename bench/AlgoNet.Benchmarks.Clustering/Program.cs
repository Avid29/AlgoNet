// Adam Dernis © 2022

using AlgoNet.Benchmarks.Clustering.Benchmarks;
using BenchmarkDotNet.Running;


Console.WriteLine("Choose a Benchmark to run.");
Console.WriteLine("MeanShift: 1");
Console.Write("Please select benchmark: ");

int input = Convert.ToInt32(Console.ReadLine());

switch (input)
{
    case 1:
        BenchmarkRunner.Run<MeanShiftBenchmarks>();
        break;
}

Console.ReadLine();