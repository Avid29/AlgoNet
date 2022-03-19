// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing insertion sort methods.
    /// </summary>
    internal static class InsertionSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        internal static void Sort<T>(T[] array) where T : IComparable<T>
            => Sort(array.AsSpan());

#if NET5_0_OR_GREATER
        internal static void Sort<T>(List<T> list) where T : IComparable<T>
            => Sort(CollectionsMarshal.AsSpan(list));
#endif

        /// <summary>
        /// Runs insertion sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
            where T : IComparable<T>
        {
            for (int i = 1; i < array.Length; i++)
            {
                T a = array[i];
                for (int j = i - 1; j >= 0;)
                {
                    if (a.CompareTo(array[j]) < 0)
                    {
                        array[j + 1] = array[j];
                        array[j] = a;
                        j--;
                    }
                    else break;
                }
            }
        }
    }
}
