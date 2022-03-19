// Adam Dernis © 2022

using AlgoNet.Benchmarks.Benchmarks;
using BenchmarkDotNet.Running;
using System;

Console.WriteLine("Choose a Benchmark to run.");
Console.WriteLine("Sorting Benchmarks: 1");
Console.WriteLine("Selection Benchmarks: 2");
Console.Write("Please select benchmark: ");

int input = Convert.ToInt32(Console.ReadLine());

switch (input)
{
    case 1:
        BenchmarkRunner.Run<SortingBenchmarks>();
        break;
    case 2:
        BenchmarkRunner.Run<SelectionBenchmarks>();
        break;
}

Console.ReadLine();