// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Methods
{
    /// <summary>
    /// A static class containing bubble sort methods.
    /// </summary>
    /// <remarks>
    /// Bubble sort is best used when an array is nearly sorted.
    /// </remarks>
    public static class BubbleSort
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
            for (int pos = 0; pos < array.Length - 1; pos++)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    ref T a = ref array[i];
                    ref T b = ref array[i + 1];
                    if (a.CompareTo(b) > 0) Swap(ref a, ref b);
                }
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }
    }
}
