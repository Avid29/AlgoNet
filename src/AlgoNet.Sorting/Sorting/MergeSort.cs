// Adam Dernis © 2022

using System;

namespace AlgoNet.Sorting
{
    /// <summary>
    /// A static class containing merge sort methods.
    /// </summary>
    public static class MergeSort
    {
        /// <inheritdoc cref="Sort{T}(Span{T})"/>
        public static void Sort<T>(T[] array) where T : IComparable => Sort(array.AsSpan());

        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="array">The array to sort.</param>
        public static void Sort<T>(Span<T> array)
            where T : IComparable
        {
            Span<T> buffer = new T[array.Length];
            SplitMerge(array, buffer);
        }

        private static Span<T> SplitMerge<T>(Span<T> array, Span<T> target, int depth = 0)
            where T : IComparable
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

        private static void Merge<T>(Span<T> left, Span<T> right, Span<T> target)
            where T : IComparable
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
 