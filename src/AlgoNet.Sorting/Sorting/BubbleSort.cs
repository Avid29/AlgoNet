// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing bubble sort methods.
    /// </summary>
    /// <remarks>
    /// Bubble sort is 2nd best (2nd to insertion sort) used when an array is nearly sorted.
    /// </remarks>
    public static class BubbleSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        public static void Sort<T>(T[] array) where T : IComparable
            => Sort(array.AsSpan());

        /// <summary>
        /// Runs bubble sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array)
            where T : IComparable
        {
            for (int pos = 0; pos < array.Length - 1; pos++)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    ref T a = ref array[i];
                    ref T b = ref array[i + 1];
                    if (a.CompareTo(b) > 0) Common.Swap(ref a, ref b);
                }
            }
        }

        /// <summary>
        /// Runs bubble sort on an <see cref="IList{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of item in the list being sorted.</typeparam>
        /// <param name="list">The <see cref="IList{T}"/> to sort.</param>
        public static void Sort<T>(IList<T> list)
            where T : IComparable
        {
            for (int pos = 0; pos < list.Count - 1; pos++)
            {
                for (int i = 0; i < list.Count- 1; i++)
                {
                    T a = list[i];
                    T b = list[i + 1];
                    if (a.CompareTo(b) > 0)
                    {
                        list[i + 1] = b;
                        list[i] = a;
                    }
                }
            }
        }
    }
}
