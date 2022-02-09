// Adam Dernis © 2022

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing counting sort methods.
    /// </summary>
    public class CountingSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T},Func{T, int},int)"/>
        public static T[] Sort<T>(T[] array, Func<T, int> valueMethod, int k) => Sort(array.AsSpan(), valueMethod, k).ToArray();

        public static Span<T> Sort<T>(Span<T> array, Func<T, int> valueMethod, int k)
        {
            Unsafe.SkipInit(out int i);
            Unsafe.SkipInit(out int value);
            Span<int> buffer = new Span<int>(new int[k]);
            
            // Count the number of items with each value
            for (i = 1; i < array.Length; i++)
            {
                value = valueMethod(array[i]);
                Debug.Assert(value < k);
                buffer[value]++;
            }

            // Convert that to the total number of values below each value
            for (i = 1; i < k; i++)
                buffer[i] = buffer[i] + buffer[i - 1];

            Span<T> result = new Span<T>(new T[array.Length]);
            for (i = array.Length - 1; i >= 1; i--)
            {
                T item = array[i];
                value = valueMethod(item);
                result[buffer[value]] = item;
                buffer[value]--;
            }

            return result;
        }
    }
}
