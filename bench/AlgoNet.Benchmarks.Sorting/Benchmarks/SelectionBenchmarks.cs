// Adam Dernis © 2022

using AlgoNet.Sorting.Select;
using AlgoNet.Sorting.Shuffle;
using BenchmarkDotNet.Attributes;

namespace AlgoNet.Benchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public class SelectionBenchmarks
    {
        private int[] _array = new int[1];
        private List<int> _list = new List<int>();
        private int _k;

        [Params(10_000)]
        public int Count;

        [Params(.25f, .5f, .75f)]
        public float Kfrac;

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
            _list = new List<int>(_array);
            _k = (int)(_array.Length * Kfrac);
        }

        [Benchmark]
        public void AlgoNet_QuickSelect()
        {
            QuickSelect.Select(_array, _k);
        }

        [Benchmark]
        public void AlgoNet_Quick3Select()
        {
            Quick3Select.Select(_array, _k);
        }
    }
}
