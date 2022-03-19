// Adam Dernis © 2022

using AlgoNet.Mathematics;
using AlgoNet.Sorting.Select;
using AlgoNet.Sorting.Shuffle;
using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoNet.Benchmarks
{
    [MemoryDiagnoser]
    public class MinMaxBenchmarks
    {
        private int[] _array = new int[1];

        [Params(10_000)]
        public int Count;

        [Params(1, 5, 10)]
        public int Occurances;

        [Params(true)]
        public bool Randomized;

        [IterationSetup]
        public void IterationSetup()
        {
            var range = Enumerable.Range(0, (Count / Occurances));
            IEnumerable<int>? progress = new List<int>();
            for (int i = 0; i < Occurances; i++)
            {
                progress = progress.Concat(range);
            }
            _array = progress.Take(Count).ToArray();

            if (Randomized) FisherYates.Shuffle(_array, 0);
        }

        [Benchmark]
        public void AlgoNet_Iterative_Max()
        {
            ExtraMath.IterativeMax(_array.AsSpan());
        }

        [Benchmark]
        public void AlgoNet_Iterative_Min()
        {
            ExtraMath.IterativeMin(_array.AsSpan());
        }

        [Benchmark]
        public void AlgoNet_QuickSelect_Max()
        {
            QuickSelect.Select(_array, _array.Length - 1);
        }

        [Benchmark]
        public void AlgoNet_QuickSelect_Min()
        {
            QuickSelect.Select(_array, 0);
        }

        [Benchmark]
        public void AlgoNet_Quick3Select_Max()
        {
            Quick3Select.Select(_array, _array.Length - 1);
        }

        [Benchmark]
        public void AlgoNet_Quick3Select_Min()
        {
            Quick3Select.Select(_array, 0);
        }

        [Benchmark]
        public void AlgoNet_Recursive_Max()
        {
            ExtraMath.RecursiveMax(_array.AsSpan());
        }

        [Benchmark]
        public void AlgoNet_Recursive_Min()
        {
            ExtraMath.RecursiveMin(_array.AsSpan());
        }

        [Benchmark]
        public void Linq_Max()
        {
            _array.Max();
        }

        [Benchmark]
        public void Linq_Min()
        {
            _array.Min();
        }
    }
}
