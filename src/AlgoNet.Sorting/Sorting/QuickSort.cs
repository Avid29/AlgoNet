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
            => Sort(array.AsSpan());

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array)
            where T : IComparable
        {
            if (array.Length <= 1) return;

            int pivot = Partition(array);

            Sort(array.Slice(0, pivot));
            Sort(array.Slice(pivot + 1));
        }

        private static int Partition<T>(Span<T> array)
            where T : IComparable
        {
            T pivot = array[array.Length - 1];
            int low = -1;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].CompareTo(pivot) < 0)
                {
                    low++;
                    Common.Swap(ref array[i], ref array[low]);
                }
            }

            low++;
            Common.Swap(ref array[low], ref array[array.Length - 1]);
            return low;
        }
    }
}
