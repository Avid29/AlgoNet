// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting.Shuffle
{
    /// <summary>
    /// A static class containing Fisher-Yates shuffle methods.
    /// </summary>
    public class FisherYates
    {
        /// <inheritdoc cref="Shuffle{T}(Span{T}, Random)"/>
        public static void Shuffle<T>(T[] array) => Shuffle(array.AsSpan(), new Random());

        /// <inheritdoc cref="Shuffle{T}(Span{T}, Random)"/>
        public static void Shuffle<T>(T[] array, int seed) => Shuffle(array.AsSpan(), new Random(seed));

        /// <inheritdoc cref="Shuffle{T}(Span{T}, Random)"/>
        public static void Shuffle<T>(T[] array, Random rand) => Shuffle(array.AsSpan(), rand);

        /// <inheritdoc cref="Shuffle{T}(Span{T}, Random)"/>
        public static void Shuffle<T>(Span<T> array) => Shuffle(array, new Random());

        /// <inheritdoc cref="Shuffle{T}(Span{T}, Random)"/>
        public static void Shuffle<T>(Span<T> array, int seed) => Shuffle(array, new Random(seed));

        /// <summary>
        /// Runs Fisher-Yates on an array.
        /// </summary>
        /// <typeparam name="T">The type of items in an array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        /// <param name="rand">The random number generator.</param>
        public static void Shuffle<T>(Span<T> array, Random rand)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int swap = rand.Next(i);
                Common.Swap(ref array[i], ref array[swap]);
            }
        }
    }
}
