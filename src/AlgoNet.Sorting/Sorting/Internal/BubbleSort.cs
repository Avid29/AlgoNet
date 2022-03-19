// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing bubble sort methods.
    /// </summary>
    internal static class BubbleSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        internal static void Sort<T>(T[] array) where T : IComparable<T>
            => Sort(array.AsSpan());

#if NET5_0_OR_GREATER
        internal static void Sort<T>(List<T> list) where T : IComparable<T>
            => Sort(CollectionsMarshal.AsSpan(list));
#endif

        /// <summary>
        /// Runs bubble sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
            where T : IComparable<T>
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
    }
}
