// Adam Dernis © 2022

using AlgoNet.Sorting.Sorting.BucketSort;
using System;

namespace AlgoNet.Sorting
{
    public class BucketSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T},Func{T, float})"/>
        public static T[] Sort<T>(T[] array, Func<T, float> valueMethod, float max) => Sort(array.AsSpan(), valueMethod, max).ToArray();

        public static Span<T> Sort<T>(Span<T> array, Func<T, float> valueMethod, float max) => Sort(array, valueMethod, 0, max, array.Length / 2);

        public static Span<T> Sort<T>(Span<T> array, Func<T, float> valueMethod, float min, float max, int steps)
        {
            BucketSortContext<T> context = new BucketSortContext<T>(min, max, steps, valueMethod);
            for (int i = 0; i < array.Length; i++) context.Insert(array[i]);

            // TODO: Sort buckets and return
            return null;
        }
    }
}
