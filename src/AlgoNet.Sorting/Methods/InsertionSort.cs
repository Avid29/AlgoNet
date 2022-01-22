// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing insertion sort methods.
    /// </summary>
    /// <remarks>
    /// Insertion sort is best used when an array is nearly sorted.
    /// </remarks>
    public static class InsertionSort
    {
        /// <inheritdoc cref="Sort{T}(T[])"/>
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
            for (int i = 1; i < array.Length; i++)
            {
                ref T a = ref array[i];
                for (int j = i - 1; j >= 0;)
                {
                    if (a.CompareTo(array[j]) > 0)
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
