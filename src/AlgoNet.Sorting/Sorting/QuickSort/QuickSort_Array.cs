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
    public static partial class QuickSort
    {
        /// QuickSort works be recursively partitioning smaller values above and greater values below a pivot.
        /// The partition step is done in place. To start the partiton, the last value is selected as the pivot.
        /// Then swaps are performed moving all values lower than the pivot to the start of the array.
        /// The first value larger than the pivot is then swapped with the pivot. As a result, all values less than
        /// the pivot are in front of the pivot and all values greater than or equal to are behind.
        /// 
        /// For example
        /// Let's sort this list
        /// | 2   4   6   1   5   3 |
        /// 
        /// Step 1
        ///     Group | - Unsorted - - - - -  | 
        ///     Value | 2   4   6   1   5   3 |
        ///     Index | 0   1   2   3   4   5 |
        ///     Pivot |                     ^ |
        /// 
        ///     In step 1 we set a pivot at the last index
        ///     
        /// 
        /// Step 2
        ///     Group | - Unsorted - - - - -  | 
        ///     Parti | 2   1   6   4   5 | 3 |
        ///     Index | 0   1   2   3   4   5 |
        ///     Pivot |                     ^ |
        ///     Swped | 2   1 | 3 | 4   5   6 |
        ///     
        ///     In step 2 we partition the entire list around 3.
        ///     The "Parti" is what the array looks like before swapping the pivot and the first value greater than it.
        /// 
        /// 
        /// Step 3
        ///     Group | Unsrt | S | Unsorted  | 
        ///     Parti | 2 | 1 | 3 | 4   5 | 6 |
        ///     Index | 0   1 | 2 | 3   4   5 |
        ///     Pivot |     ^ |   |         ^ |
        ///     Swped | 1   2 | 3 | 4   5   6 |
        ///     
        ///     In step 3 the previous pivot is in its correct sorted position, so we'll leave it alone.
        ///     We'll now repeat the previous step on both the values above and below the old pivot.
        ///     The values greater than 3 are already sorted, and the values below need only a single swap.

        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        public static void Sort<T>(T[] array) where T : IComparable<T> => Sort(array.AsSpan());

        /// <inheritdoc cref="Select{T}(Span{T},int)"/>
        public static T Select<T>(T[] array, int k) where T : IComparable<T> => Select(array.AsSpan(), k);

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array)
            where T : IComparable<T>
        {
            // Nothing to sort (base case)
            if (array.Length <= 1) return;

            // Partition values before and after the pivot.
            int pivot = Partition(array);

            // Sort values before the pivot
            Sort(array.Slice(0, pivot));

            // Sort values after the pivot
            Sort(array.Slice(pivot + 1));
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
            where T : IComparable<T>
        {
            while (true)
            {
                // Nothing left to sort (base case)
                if (array.Length == 1) return array[0];
                else if (array.Length < 1) return default;

                // Partition around the center value.
                int center = array.Length / 2;
                center = SelectPartition(array, center);

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

        private static int Partition<T>(Span<T> array)
            where T : IComparable<T>
        {
            // Cache the pivot value
            T pivot = array[array.Length - 1];

            // Track the index of the split between above and below.
            int low = 0;

            for (int i = 0; i < array.Length - 1; i++)
            {
                // If the value is lower than the pivot.
                if (array[i].CompareTo(pivot) < 0)
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

        /// <remarks>
        /// Pre-swaps pivot index and uses less than or equal to comparison.
        /// This is partition is slower because it uses more swaps, but is neccesary for Select.
        /// </remarks>
        private static int SelectPartition<T>(Span<T> array, int pivotIndex)
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
