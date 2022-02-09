// Adam Dernis © 2022

using AlgoNet.Sorting;
using AlgoNet.Sorting.Shuffle;
using BenchmarkDotNet.Attributes;

namespace AlgoNet.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class SortingBenchmarks
    {
        private int[] _array = new int[1];

        [Params(10, 100, 1_000, 10_000)]
        public int Numbers;

        [Params(true, false)]
        public bool Randomized;

        [IterationSetup]
        public void IterationSetup()
        {
            _array = Enumerable.Range(0, Numbers).ToArray();
            if (Randomized) FisherYates.Shuffle(_array);
        }

        [Benchmark]
        public void AlgoNet_QuickSort()
        {
            QuickSort.Sort(_array);
        }

        [Benchmark]
        public void AlgoNet_MergeSort()
        {
            MergeSort.Sort(_array);
        }

        [Benchmark]
        public void Array_Sort()
        {
            Array.Sort(_array);
        }
    }
}
