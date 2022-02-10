﻿// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing selection sort methods.
    /// </summary>
    internal static class SelectionSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        internal static void Sort<T>(T[] array) where T : IComparable<T> => Sort(array.AsSpan());

        /// <summary>
        /// Runs selection sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
            where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int iMin = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[iMin]) < 0)
                    {
                        iMin = j;
                    }
                }

                Common.Swap(ref array[i], ref array[iMin]);
            }
        }
    }
}