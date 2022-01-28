// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing quick sort methods.
    /// </summary>
    /// <remarks>
    /// This is a 2-way partitioned quick sort. This has very little overhead, but is slower than 3-way.
    /// </remarks>
    public static class QuickSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        public static void Sort<T>(T[] array) where T : IComparable => Sort(array.AsSpan());

        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static T Select<T>(T[] array, int k) where T : IComparable => Select(array.AsSpan(), k);

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array)
            where T : IComparable
        {
            if (array.Length <= 1) return;

            int center = Partition(array);

            Sort(array.Slice(0, center));
            Sort(array.Slice(center + 1));
        }

        /// <summary>
        /// Runs quick select on an array.
        /// </summary>
        /// <remarks>
        /// After running all values before k will be less than k and all after k below will be greater.
        /// </remarks>
        /// <typeparam name="T">The type of item in the array.</typeparam>
        /// <param name="array">The array to select on.</param>
        /// <param name="k">The position to select.</param>
        /// <returns>The kth smallest item in the array.</returns>
        public static T Select<T>(Span<T> array, int k)
            where T : IComparable
        {
            if (array.Length == 1) return array[0];
            else if (array.Length < 1) return default;

            int center = array.Length / 2;
            center = Partition(array, center);

            if (k == center) return array[k];
            else if (k < center) return Select(array.Slice(0, center), k);
            else return Select(array.Slice(center), k - center);
        }

        private static int Partition<T>(Span<T> array, int pivotIndex)
            where T : IComparable
        {
            Common.Swap(ref array[pivotIndex], ref array[array.Length - 1]);
            return Partition(array);
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
