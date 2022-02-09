// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Sorting.BucketSort
{
    internal ref struct BucketSortContext<T>
    {
        private LinkedNode<T>[] _array;
        private Func<T, float> _valueFunc;
        private float _min;
        private float _co;

        public BucketSortContext(float min, float max, int steps, Func<T, float> func)
        {
            _array = new LinkedNode<T>[steps];

            float range = max - min;
            _min = min;
            _co = steps / range;

            _valueFunc = func;
        }

        public void Insert(T item)
        {
            float value = _valueFunc(item);
            int bucket = (int)((value - _min) * _co);

            LinkedNode<T> old = _array[bucket];
            _array[bucket] = new LinkedNode<T>(item);
            _array[bucket].Previous = old;
        }
    }
}
