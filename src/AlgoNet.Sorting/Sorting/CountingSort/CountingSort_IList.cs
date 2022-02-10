// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlgoNet.Sorting
{
    public static partial class CountingSort
    {
        /// <summary>
        /// Sorts an array with counting sort
        /// </summary>
        /// <typeparam name="T">The type of item in the list.</typeparam>
        /// <param name="list">The list to sort from</param>
        /// <param name="valueMethod">A function that converts a <typeparamref name="T"/> to an int lower than <paramref name="k"/>.</param>
        /// <param name="k">The max output from <paramref name="valueMethod"/>.</param>
        /// <returns>An sorted array of the values in <paramref name="list"/>.</returns>
        public static T[] Sort<T>(IList<T> list, Func<T, int> valueMethod, int k)
        {
            Unsafe.SkipInit(out int i);
            Unsafe.SkipInit(out int value);
            Span<int> buffer = new int[k];
            
            // Count the number of items with each value
            for (i = 0; i < list.Count; i++)
            {
                value = valueMethod(list[i]);
                Debug.Assert(value < k && value >= 0);
                buffer[value]++;
            }

            // Convert that to the total number of values below each value
            for (i = 1; i < k; i++)
                buffer[i] = buffer[i] + buffer[i - 1];

            // Check the buffer for the position to place each value.
            // Starting at the back ensures a stable sort.
            T[] result = new T[list.Count];
            for (i = list.Count - 1; i >= 0; i--)
            {
                T item = list[i];
                value = valueMethod(item);
                buffer[value]--;
                result[buffer[value]] = item;
            }

            return result;
        }
    }
}
