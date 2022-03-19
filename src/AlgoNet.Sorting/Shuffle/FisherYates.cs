// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AlgoNet.Sorting.Shuffle
{
    /// <summary>
    /// A static class containing Fisher-Yates shuffle methods.
    /// </summary>
    public class FisherYates
    {
        /// Fisher-Yates algorithm is a shuffling algorithm guarenteed to give an equal chance of any value to end in any position.
        /// This is accomplished by selecting a random value from else where in the unshuffled list and swapping with that value.
        ///
        /// For example
        /// Let's shuffled this list
        /// | 1   2   3   4   5 |
        /// 
        /// Step 1
        ///     Rand from 0 to 4: 3
        ///     Group |   Unshuffled      |
        ///     Value | 1   2   3   4   5 |
        ///     Index | 0   1   2   3   4 |
        ///     Swap  |             ^   ^ |
        ///
        /// Step 2
        ///     Rand from 0 to 3: 1
        ///     Group |   Unshuffled  | Sh|
        ///     Value | 1   2   3   5 | 4 |
        ///     Index | 0   1   2   3 | 4 |
        ///     Swap  |     ^       ^ |   |
        ///
        /// Step 3
        ///     Rand from 0 to 2: 2
        ///     Group | Unshuffle | Shuff |
        ///     Value | 1   5   3 | 2   4 |
        ///     Index | 0   1   2 | 3   4 |
        ///     Swap  |         ^ |       |
        ///
        /// Step 3
        ///     Rand from 0 to 1: 0
        ///     Group | Unshu |  Shuffled |
        ///     Value | 1   5 | 3   2   4 |
        ///     Index | 0   1 | 2   3   4 |
        ///     Swap  | ^   ^ |           |
        ///     
        /// Done
        ///     Group |     Shuffled      |
        ///     Value | 5   1   3   2   4 |

        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(T[] array)
            => Shuffle(array.AsSpan(), new Random());

        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(T[] array, int seed)
            => Shuffle(array.AsSpan(), new Random(seed));

        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(T[] array, Random rand)
            => Shuffle(array.AsSpan(), rand);

        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(Span<T> array)
            => Shuffle(array, new Random());

        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(Span<T> array, int seed)
            => Shuffle(array, new Random(seed));

#if NET5_0_OR_GREATER
        /// <inheritdoc cref="Shuffle{T}(Span{T},Random)"/>
        public static void Shuffle<T>(List<T> list) where T : IComparable<T>
            => Shuffle(CollectionsMarshal.AsSpan(list));
#endif

        /// <summary>
        /// Runs Fisher-Yates on an array.
        /// </summary>
        /// <typeparam name="T">The type of items in an array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        /// <param name="rand">The random number generator.</param>
        public static void Shuffle<T>(Span<T> array, Random rand)
        {
            // Iterate from the back for each value except the first.
            for (int i = array.Length - 1; i > 0; i--)
            {
                // Swap with value from random index that hasn't already been selected.
                int swap = rand.Next(i);
                Common.Swap(ref array[i], ref array[swap]);
            }
        }
    }
}
