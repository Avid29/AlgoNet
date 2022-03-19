// Adam Dernis © 2022

using AlgoNet.Benchmarks;
using BenchmarkDotNet.Running;
using System;


Console.WriteLine("Choose a Benchmark to run.");
Console.WriteLine("MinMax: 1");
Console.Write("Please select benchmark: ");

int input = Convert.ToInt32(Console.ReadLine());

switch (input)
{
    case 1:
        BenchmarkRunner.Run<MinMaxBenchmarks>();
        break;
}

Console.ReadLine();