using System;

namespace AlgoNet.Mathematics.Generic
{
    public static partial class ExtraMath
    {
        /// <summary>
        /// Returns the largest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The largest number</returns>
        public static T Max<T>(params T[] nums)
            where T : INumber<T> => IterativeMax(nums.AsSpan())!;

        /// <summary>
        /// Returns the smallest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The smallest number</returns>
        public static T Min<T>(params T[] nums)
            where T : INumber<T> => IterativeMin(nums.AsSpan());

        internal static T IterativeMax<T>(Span<T> nums)
            where T : INumber<T>
        {
            T max = nums[0];
            for (int i = 1; i < nums.Length; i++)
                max = T.Max(max, nums[i]);
            return max;
        }

        internal static T IterativeMin<T>(Span<T> nums)
            where T : INumber<T>
        {
            T min = nums[0];
            for (int i = 1; i < nums.Length; i++)
                min = T.Min(min, nums[i]);
            return min;
        }

        internal static T RecursiveMax<T>(Span<T> nums)
            where T : INumber<T>
        {
            if (nums.Length == 1) return nums[0];

            int mid = nums.Length / 2;

            T left = RecursiveMax(nums.Slice(0, mid));
            T right = RecursiveMax(nums.Slice(mid));
            return T.Max(left, right);
        }

        internal static T RecursiveMin<T>(Span<T> nums)
            where T : INumber<T>
        {
            if (nums.Length == 1) return nums[0];

            int mid = nums.Length / 2;

            T left = RecursiveMin(nums.Slice(0, mid));
            T right = RecursiveMin(nums.Slice(mid));
            return T.Min(left, right);
        }
    }
}
