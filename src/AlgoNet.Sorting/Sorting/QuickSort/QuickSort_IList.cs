// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing quick sort methods.
    /// </summary>
    /// <remarks>
    /// This is a 2-way partitioned quick sort. This has very little overhead, but is slower than 3-way.
    /// </remarks>
    public static partial class QuickSort
    {
        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(IList<T> array) where T : IComparable<T> => Sort(array, 0, array.Count - 1);

        private static void Sort<T>(IList<T> array, int low, int high)
            where T : IComparable<T>
        {
            // Nothing to sort (base case)
            if (low >= high) return;

            // Partition values before and after the pivot.
            int pivot = Partition(array, low, high);

            // Sort values before the pivot
            Sort(array, low, pivot-1);

            // Sort values after the pivot
            Sort(array, pivot + 1, high);
        }

        private static int Partition<T>(IList<T> array, int low, int high)
            where T : IComparable<T>
        {
            // Cache the pivot value
            T pivot = array[high];
            T swap;

            // The low index will now be used to reprsent the split between values
            // greater than and lower than the pivot
            for (int i = low; i < high; i++)
            {
                // If the value is lower than the pivot.
                if (array[i].CompareTo(pivot) < 0)
                {
                    // Swap value to the lower than pivot partition and increment the partiton size
                    swap = array[i];
                    array[i] = array[low];
                    array[low] = swap;
                    low++;
                }
            }

            // Swap the pivot with the first value great than it.
            // (Swaps with itself if all values are lower)
            swap = array[high];
            array[high] = array[low];
            array[low] = swap;
            return low;
        }
    }
}
