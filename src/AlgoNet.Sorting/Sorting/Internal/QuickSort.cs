// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing quick sort methods.
    /// </summary>
    /// <remarks>
    /// This is a 2-way partitioned quick sort. This has very little overhead, but is slower than 3-way.
    /// </remarks>
    internal static partial class QuickSort
    {
        /// QuickSort works by recursively partitioning smaller values above and greater values below a pivot.
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
        internal static void Sort<T>(T[] array) where T : IComparable<T>
            => Sort(array.AsSpan());

        /// <inheritdoc cref="SortAsync{T}(Memory{T})"/>
        internal static Task SortAsync<T>(T[] array) where T : IComparable<T>
            => SortAsync(array.AsMemory());


#if NET5_0_OR_GREATER
        internal static void Sort<T>(List<T> list) where T : IComparable<T>
            => Sort(CollectionsMarshal.AsSpan(list));
#endif

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
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
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static async Task SortAsync<T>(Memory<T> array)
            where T : IComparable<T>
        {
            // Nothing to sort (base case)
            if (array.Length <= 1) return;

            // Partition values before and after the pivot.
            int pivot = Partition(array.Span);

            // Sort values before the pivot
            await Task.Run(() => SortAsync(array.Slice(0, pivot)));

            // Sort values after the pivot
            await Task.Run(() => SortAsync(array.Slice(pivot + 1)));
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
    }
}
