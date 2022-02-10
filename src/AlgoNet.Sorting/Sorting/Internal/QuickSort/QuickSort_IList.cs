// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Sorting
{
    internal static partial class QuickSort
    {
        /// <summary>
        /// Runs quick sort on a list.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="list">The list to sort.</param>
        internal static void Sort<T>(IList<T> list) where T : IComparable<T> => Sort(list, 0, list.Count - 1);

        private static void Sort<T>(IList<T> list, int low, int high)
            where T : IComparable<T>
        {
            // Nothing to sort (base case)
            if (low >= high) return;

            // Partition values before and after the pivot.
            int pivot = Partition(list, low, high);

            // Sort values before the pivot
            Sort(list, low, pivot-1);

            // Sort values after the pivot
            Sort(list, pivot + 1, high);
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
