// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Sorting
{
    public static partial class MergeSort
    {
        /// <summary>
        /// Runs quick sort on an array.
        /// </summary>
        /// <typeparam name="T">The type of item in the array being sorted.</typeparam>
        /// <param name="list">The array to sort.</param>
        public static void Sort<T>(IList<T> list)
            where T : IComparable<T>
        {
            IList<T> buffer = new List<T>(new T[list.Count]);
            SplitMerge(list, buffer, 0, list.Count);
        }

        private static IList<T> SplitMerge<T>(IList<T> list, IList<T> target, int low, int high, int depth = 0)
            where T : IComparable<T>
        {
            // Base case
            if (low + 1 == high)
            {
                target[low] = list[low];
                if (depth % 2 == 1)
                {
                    return target;
                } else
                {
                    return list;
                }
            }

            // Split the array recursively
            int mid = (high + low) / 2;
            int newDepth = depth + 1;
            IList<T> left = SplitMerge(list, target, low, mid, newDepth);
            IList<T> right = SplitMerge(list, target, mid, high, newDepth);

            // Use the source array at even depths and target at odd.
            // This ensures the final merge is on the source array.
            target = depth % 2 == 0 ? list : target;
            Merge(left, right, target, low, mid, high);
            return target;
        }

        private static void Merge<T>(IList<T> left, IList<T> right, IList<T> target, int low, int mid, int high)
            where T : IComparable<T>
        {
            // Track the left, right, and target offsets
            int l = low;
            int r = mid;
            int x = low;

            // Track the span containing the lower value and its offset.
            ref int i = ref l;
            ref IList<T> src = ref left;
            int max = mid;

            do
            {
                // Find smaller value.
                if (left[l].CompareTo(left[r]) <= 0)
                {
                    i = ref l;
                    src = ref left;
                    max = mid;
                }
                else
                {
                    i = ref r;
                    src = ref right;
                    max = high;
                }

                // Add smaller value
                target[x] = src[i];
                i++;
                x++;

            } while (i != max);

            // Change src and i to the value with the greater values
            i = i == l ? r : l;

            // Copy remaining values.
            while (x < high)
            {
                target[x] = src[i];
                i++;
                x++;
            }
        }
    }
}
