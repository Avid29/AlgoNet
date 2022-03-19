// Adam Dernis © 2022

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing merge sort methods.
    /// </summary>
    internal static partial class MergeSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        internal static void Sort<T>(T[] array) where T : IComparable<T>
            => Sort(array.AsSpan());

        /// <inheritdoc cref="SortAsync{T}(Memory{T})"/>
        internal static Task SortAsync<T>(T[] array) where T : IComparable<T>
            => SortAsync(array.AsMemory());


#if NET5_0_OR_GREATER
        internal static void Sort<T>(List<T> list) where T : IComparable<T>
            => Sort(CollectionsMarshal.AsSpan(list));
#endif


        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static void Sort<T>(Span<T> array)
            where T : IComparable<T>
        {
            Span<T> buffer = new T[array.Length];
            SplitMerge(array, buffer);
        }

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        internal static async Task SortAsync<T>(Memory<T> array)
            where T : IComparable<T>
        {
            Memory<T> buffer = new T[array.Length];
            await SplitMergeAsync(array, buffer);
        }

        private static Span<T> SplitMerge<T>(Span<T> array, Span<T> target, int depth = 0)
            where T : IComparable<T>
        {
            // Base case
            if (array.Length == 1)
            {
                if (depth % 2 == 1)
                {
                    target[0] = array[0];
                    return target;
                } else
                {
                    return array;
                }
            }

            // Split the array recursively
            int mid = array.Length / 2;
            int newDepth = depth + 1;
            Span<T> left = SplitMerge(array.Slice(0, mid), target.Slice(0, mid), newDepth);
            Span<T> right = SplitMerge(array.Slice(mid), target.Slice(mid), newDepth);

            // Use the source array at even depths and target at odd.
            // This ensures the final merge is on the source array.
            target = depth % 2 == 0 ? array : target;
            Merge(left, right, target);
            return target;
        }

        private static async Task<Memory<T>> SplitMergeAsync<T>(Memory<T> array, Memory<T> target, int depth = 0)
            where T : IComparable<T>
        {
            // Base case
            if (array.Length == 1)
            {
                if (depth % 2 == 1)
                {
                    target.Span[0] = array.Span[0];
                    return target;
                } else
                {
                    return array;
                }
            }

            // Split the array recursively
            int mid = array.Length / 2;
            int newDepth = depth + 1;
            await Task.Run(() => SplitMergeAsync(array.Slice(0, mid), target.Slice(0, mid), newDepth));
            await Task.Run(() => SplitMergeAsync(array.Slice(mid), target.Slice(mid), newDepth));

            // Use the source array at even depths and target at odd.
            // This ensures the final merge is on the source array.
            Memory<T> left;
            Memory<T> right;
            if (depth % 2 == 0)
            {
                left = target.Slice(0, mid);
                right = target.Slice(mid);
                target = array;
            }
            else
            {
                left = array.Slice(0, mid);
                right = array.Slice(mid);
            }

            Merge(left.Span, right.Span, target.Span);
            return target;
        }

        private static void Merge<T>(Span<T> left, Span<T> right, Span<T> target)
            where T : IComparable<T>
        {
            // Track the left, right, and target offsets
            int l = 0;
            int r = 0;
            int x = 0;

            // Track the span containing the lower value and its offset.
            ref Span<T> src = ref left;
            ref int i = ref l;

            do
            {
                // Find smaller value.
                if (left[l].CompareTo(right[r]) <= 0)
                {
                    i = ref l;
                    src = ref left;
                }
                else
                {
                    i = ref r;
                    src = ref right;
                }

                // Add smaller value
                target[x] = src[i];
                i++;
                x++;

            } while (i != src.Length);

            // Change src and i to the value with the greater values
            src = src == left ? right : left;
            i = i == l ? r : l;

            // Copy remaining values.
            src.Slice(i).CopyTo(target.Slice(x));
        }
    }
}
 