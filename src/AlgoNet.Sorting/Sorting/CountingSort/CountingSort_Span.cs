// Adam Dernis © 2022

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing counting sort methods.
    /// </summary>
    public static partial class CountingSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T},Func{T, int},int)"/>
        public static T[] Sort<T>(T[] array, Func<T, int> valueMethod, int k)
        {
            T[] result = new T[array.Length];
            Sort(array.AsSpan(), result.AsSpan(), valueMethod, k);
            return result;
        }

        /// <summary>
        /// Sorts an array using Counting Sort.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to sort.</param>
        /// <param name="valueMethod">A function that grabs an int less than <paramref name="k"/> from a <typeparamref name="T"/> in the array.</param>
        /// <param name="k">The max value output of the <paramref name="valueMethod"/>.</param>
        /// <returns>A sorted clone of <paramref name="array"/>.</returns>
        public static Span<T> Sort<T>(Span<T> array, Func<T, int> valueMethod, int k)
        {
            Span<T> result = new T[array.Length];
            Sort(array, result, valueMethod, k);
            return result;
        }

        private static Span<T> Sort<T>(Span<T> array, Span<T> result, Func<T, int> valueMethod, int k)
        {
            Unsafe.SkipInit(out int i);
            Unsafe.SkipInit(out int value);
            Span<int> buffer = new int[k];
            
            // Count the number of items with each value
            for (i = 0; i < array.Length; i++)
            {
                value = valueMethod(array[i]);
                Debug.Assert(value < k && value >= 0);
                buffer[value]++;
            }

            // Convert that to the total number of values below each value
            for (i = 1; i < k; i++)
                buffer[i] = buffer[i] + buffer[i - 1];

            // Check the buffer for the position to place each value.
            // Starting at the back ensures a stable sort.
            for (i = array.Length - 1; i >= 0; i--)
            {
                T item = array[i];
                value = valueMethod(item);
                buffer[value]--;
                result[buffer[value]] = item;
            }

            return result;
        }
    }
}
