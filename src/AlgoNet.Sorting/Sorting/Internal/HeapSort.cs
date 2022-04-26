// Adam Dernis © 2022

namespace AlgoNet.Sorting.Sorting.Internal
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
    /// Step 10: (9 skipped)
    ///     Group | Heap  | - Sorted - - -| 
    ///     Value | 2   1 | 3   4   5   6 |
    ///     Index | 0   1 | 2   3   4   5 |
    /// 
    ///     Tree:
    ///             2(0)
    ///         1(1)
    ///         
    ///     At this step we have two items left in a max heap, so we guarentee it is reverse sorted.
    ///     Just swap the two values.


    /// <summary>
    /// A static class containing heap sort methods.
    /// </summary>
    internal static class HeapSort
    {

    }
}
