// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Select
{
    /// <summary>
    /// A static class containing quick select methods.
    /// </summary>
    public static class QuickSelect
    {
        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static T? Select<T>(T[] array, int k) where T : IComparable<T> => Select(array.AsSpan(), k);

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
        public static T? Select<T>(Span<T> array, int k)
            where T : IComparable<T>
        {
            while (true)
            {
                // Nothing left to sort (base case)
                if (array.Length == 1) return array[0];
                else if (array.Length < 1) return default;

                // Partition around the center value.
                int center = array.Length / 2;
                center = Partition(array, center);

                // The kth item has been found
                if (k == center || center == 0)
                    return array[k];

                // The kth item is before the new center
                if (k < center)
                {
                    array = array.Slice(0, center);
                    continue;
                }

                // The kth item is after the new center
                array = array.Slice(center);
                k -= center;
                continue;
            }
        }

        /// <remarks>
        /// Pre-swaps pivot index and uses less than or equal to comparison.
        /// This is partition is slower because it uses more swaps, but is neccesary for Select.
        /// </remarks>
        private static int Partition<T>(Span<T> array, int pivotIndex)
            where T : IComparable<T>
        {
            // Swap the pivot index with the last index
            // Then run Partition where the last index is the pivot.
            Common.Swap(ref array[pivotIndex], ref array[array.Length - 1]);

            // Cache the pivot value
            T pivot = array[array.Length - 1];

            // Track the index of the split between above and below.
            int low = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                // If the value is lower than the pivot.
                if (array[i].CompareTo(pivot) <= 0)
                {
                    // Swap value to the lower than pivot partition and increment the partiton size
                    Common.Swap(ref array[i], ref array[low]);
                    low++;
                }
            }

            // Swap the pivot with the first value great than it.
            // (Swaps with itself if all values are lower)
            Common.Swap(ref array[low], ref array[array.Length - 1]);
            return low;
        }
    }
}
