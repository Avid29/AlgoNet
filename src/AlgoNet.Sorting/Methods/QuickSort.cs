// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing bubble sort methods.
    /// </summary>
    /// <remarks>
    /// This is a 2-way partitioned quick sort. This has very little overhead, but is slower than 3-way.
    /// </remarks>
    public static class QuickSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        public static void Sort<T>(T[] array) where T : IComparable
            => Sort<T>(array, 0, array.Length - 1);

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array) where T : IComparable
            => Sort(array, 0, array.Length - 1);

        private static void Sort<T>(Span<T> array, int l, int r)
            where T : IComparable
        {
            // Modified version of https://www.cs.princeton.edu/~rs/talks/QuicksortIsOptimal.pdf
            int i = -1;
            int j = r;
            if (j <= 1) return;
            T v = array[j];
            while (true)
            {
                while (array[++i].CompareTo(v) < 0);
                while (v.CompareTo(array[--j]) < 0) if (j == l) break;
                if (i >= j) break;
                Common.Swap(ref array[i], ref array[j]);
            }
            Common.Swap(ref array[i], ref array[r]);
            Sort(array, l, i-1);
            Sort(array, i+1, r);
        }
    }
}
