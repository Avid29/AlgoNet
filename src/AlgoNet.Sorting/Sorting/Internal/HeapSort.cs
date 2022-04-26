// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgoNet.Sorting
{
    /// Heap sort works by converting the array into a heap, then reading it back into an array.
    /// This can be done in place by heapifying the array from the front, then reading into the back
    /// 
    /// For example:
    /// Let's sort this list
    /// | 2   4   6   1   5   3 |
    /// 
    /// Step 1:
    ///     Group |Hea| - Unsorted - - -  | 
    ///     Value | 2 | 4   6   1   5   3 |
    ///     Index | 0 | 1   2   3   4   5 |
    /// 
    ///     Tree:
    ///             2
    /// 
    ///     In step 1 we have a mostly unsorted list, but can consider the first value the max in the heap.
    ///     The heap is actually stored as a flat array, but we'll also show it as a binary tree to improve understanding.
    ///     
    /// Step 2:
    ///     Group | Heap  | - Unsorted -  | 
    ///     Value | 4   2 | 6   1   5   3 |
    ///     Index | 0   1 | 2   3   4   5 |
    /// 
    ///     Tree:
    ///             4(0)
    ///         2(1)
    ///         
    ///     In steps 2-6 we'll add one value at a time to the heap, then heapify to ensure every value is smaller than its parent.
    ///     We can heapify up recurisvely by swapping an item and its parent if the child is greater than the parent.
    ///    
    /// Step 6: (3,4,5 skipped)
    ///     Group | - Heap - - - - - - -  | 
    ///     Value | 6   5   4   1   2   3 |
    ///     Index | 0   1   2   3   4   5 |
    /// 
    ///     Tree:
    ///                6(0)
    ///         5(1)         4(2)
    ///      1(3)  2(4)    3(5)
    /// 
    ///     Now that we have a max heap, we can write each item to the back of the array.
    /// 
    /// Step 7: 
    ///     Group | - Heap - - - - - -|Sor| 
    ///     Value | 5   3   4   1   2 | 6 |
    ///     Index | 0   1   2   3   4 | 5 |
    /// 
    ///     Tree:
    ///                5(0)
    ///         3(1)         4(2)
    ///      1(3)  2(4)   
    /// 
    ///     In steps 7-12 we'll pop the max value, then move the last value still on the heap to the top and heapify down
    ///     We can heapify down by swapping a value with the largest of its childrn, until the value is larger than all its children, or has none.
    /// 
    /// Step 8: 
    ///     Group | - Heap - - - -|Sorted | 
    ///     Value | 4   3   2   1 | 5   6 |
    ///     Index | 0   1   2   3 | 4   5 |
    /// 
    ///     Tree:
    ///                4(0)
    ///         3(1)         2(2)
    ///      1(3)
    /// 
    /// Step 11: (9,10,11 skipped)
    ///     Group | - Sorted - - - - - -  | 
    ///     Value | 1 | 2   3   4   5   6 |
    ///     Index | 0 | 1   2   3   4   5 |
    ///         
    ///     At this step we have just one item left in the heap, so we can leave it and call the entire space sorted.


    /// <summary>
    /// A static class containing heap sort methods.
    /// </summary>
    internal static class HeapSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        internal static void Sort<T>(T[] array) where T : IComparable<T>
            => Sort(array.AsSpan());


#if NET5_0_OR_GREATER
        internal static void Sort<T>(List<T> list) where T : IComparable<T>
            => Sort(CollectionsMarshal.AsSpan(list));
#endif

        /// <summary>
        /// Runs heap sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
            where T : IComparable<T>
        {
            // Heapify
            for (int i = 2; i <= array.Length; i++)
            {
                HeapifyUp(array.Slice(0, i));
            }
            
            // Unheapify
            for (int i = array.Length - 1; i >= 1; i--)
            {
                // Pop the top, then heapify down
                Common.Swap(ref array[i], ref array[0]);
                HeapifyDown(array.Slice(0, i));
            }
        }

        private static void HeapifyUp<T>(Span<T> array)
            where T : IComparable<T>
        {
            // Start at the last element
            int n = array.Length - 1;

            // Stop if the value has no parent
            while (n != 0)
            {
                int parent = ((n + 1) / 2) - 1;

                // If the value is smaller than its parent, the array is a heap
                if (array[n].CompareTo(array[parent]) <= 0)
                {
                    return;
                }

                // Swap the value with its parent
                Common.Swap(ref array[n], ref array[parent]);
                n = parent;
            }
        }

        private static void HeapifyDown<T>(Span<T> array)
            where T : IComparable<T>
        {
            int n = 0;

            while (true)
            {
                int right = (n + 1) * 2;
                int left = right - 1;

                // The value has no children
                if (left >= array.Length)
                {
                    return;
                }

                // Find the larger of the children and track its index
                int larger = left;
                if (right < array.Length && array[right].CompareTo(array[left]) >= 0)
                {
                    larger = right;
                }

                // If the value is larger than its larger child, the array is a heap
                if (array[n].CompareTo(array[larger]) >= 0)
                {
                    return;
                }

                // Swap the value with its largest child
                Common.Swap(ref array[n], ref array[larger]);
                n = larger;
            }
        }
    }
}
