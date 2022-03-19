// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgoNet.Sorting.Select
{
    /// <summary>
    /// A static class containing quick select methods.
    /// </summary>
    public static class QuickSelect
    {
        /// Quick Select finds the kth greatest value in an array and sets all values greater than
        /// k before and all values less than k after it.
        /// This is done by recursively partitioning an array around a pivot sorting only the section containing the kth element.
        /// 
        /// For example
        /// Let's select K = 2 from this list
        /// | 2   4   6   1   5   3 |
        /// 
        /// Step 1
        ///     Group | - Unpartitioned - - - | 
        ///     Value | 2   4   6   3   5   1 |
        ///     Index | 0   1   2   3   4   5 |
        ///     Pivot |             ^       ^ |
        ///     
        ///     In step 1 we selected the middle index (6 / 2 = 3) as the pivot
        ///     Then we swapped it with the last index for partitioning
        ///     
        /// Step 2
        ///     Group | L | - Contains kth -  | 
        ///     Value | 1 | 4   6   2   5   3 |
        ///     Index | 0 | 1   2   3   4   5 |
        ///     Pivot |   |         ^       ^ |
        ///     
        ///     In step 2 we partitioned the array so values less than the pivot are on left of it
        ///     and values greater than the pivot are to its right.
        ///     There were no values smaller than the pivot.
        ///     We also sliced the section that contains k to select in there.
        ///     
        /// Step 3
        ///     Group | Lower | K |  Greater  | 
        ///     Value | 1   2 | 3 | 4   5   6 |
        ///     Index | 0   1 | 2 | 3   4   5 |
        ///     Pivot |       |   |           |
        ///     
        ///     In step 3 we paritioned the values that contained K and found that our pivot was k.
        ///     In this case the list happens to be sorted in full, but that is not guarenteed and is unlikely for larger lists.

        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static T? Select<T>(T[] array, int k) where T : IComparable<T>
            => Select(array.AsSpan(), k);

#if NET5_0_OR_GREATER
        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static void Select<T>(List<T> list, int k) where T : IComparable<T>
            => Select(CollectionsMarshal.AsSpan(list), k);
#endif

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
