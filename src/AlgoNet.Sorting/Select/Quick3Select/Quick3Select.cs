// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Select
{
    public static class Quick3Select
    {
        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static T Select<T>(T[] array, int k) where T : IComparable<T> => Select(array.AsSpan(), k);

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
            where T : IComparable<T>
        {
            while (true)
            {
                // Nothing left to sort (base case)
                if (array.Length == 1) return array[0];
                else if (array.Length < 1) return default;

                // Partition around the center value.
                int center = array.Length / 2;
                (int,int) split = Partition3(array, center);

                // The kth item is in the equal partition
                if (k > split.Item1 && k < split.Item2)
                    return array[k];

                // The kth item is in the smaller partition
                if (k < split.Item1)
                {
                    array = array.Slice(0, split.Item1);
                    continue;
                }

                // The kth item is in the greater partition
                array = array.Slice(split.Item2);
                k -= split.Item2;
                continue;
            }
        }

        /// <remarks>
        /// Paritions array into 3 sections. Less than, equal to, and greater than.
        /// </remarks>
        /// <returns>A tuple containing the start and end of the equal section.</returns>
        private static (int,int) Partition3<T>(Span<T> array, int pivotIndex)
            where T : IComparable<T>
        {
            // Cache the pivot value
            T pivot = array[pivotIndex];

            // Track the indicies splitting low, mid, unallocated, and high
            int low = 0;
            int mid = 0;
            int high = array.Length - 1;

            while (mid <= high)
            {
                // If the value is lower than the pivot.
                if (array[mid].CompareTo(pivot) < 0)
                {
                    // Swap value and increase less than partition size.
                    Common.Swap(ref array[mid], ref array[low]);
                    low++;
                    mid++;
                } else if (array[mid].CompareTo(pivot) > 0)
                {
                    // Swap value and increase greater than parition size.
                    Common.Swap(ref array[mid], ref array[high]);
                    high--;
                }
                else
                {
                    // Increase equal parition size
                    mid++;
                }
            }
            return (low, high);
        }
    }
}
