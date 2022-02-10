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

        //[Params(10, 100, 1_000, 10_000)]
        [Params(10_000)]
        public int Count;

        //[Params(true, false)]
        [Params(true)]
        public bool Randomized;

        [IterationSetup]
        public void IterationSetup()
        {
            _array = Enumerable.Range(0, Count-1).ToArray();
            if (Randomized) FisherYates.Shuffle(_array);
        }

        [Benchmark]
        public void AlgoNet_CountingSort()
        {
            CountingSort.Sort(_array, x => x, _array.Length);
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
        public void AlgoNet_InsertionSort()
        {
            InsertionSort.Sort(_array);
        }

        //[Benchmark]
        //public void AlgoNet_SelectionSort()
        //{
        //    SelectionSort.Sort(_array);
        //}

        //[Benchmark]
        //public void AlgoNet_BubbleSort()
        //{
        //    BubbleSort.Sort(_array);
        //}

        [Benchmark]
        public void Array_Sort()
        {
            Array.Sort(_array);
        }

        [Benchmark]
        public void List_Sort()
        {
            _array.ToList().Sort();
        }

        [Benchmark]
        public void Linq_OrderBy()
        {
            _array = _array.OrderBy(x => x).ToArray();
        }
    }
}
