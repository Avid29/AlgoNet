// Adam Dernis © 2022

using AlgoNet.Sorting.Select;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AlgoNet.Mathematics
{
    /// <summary>
    /// Extra math operations
    /// </summary>
    public static class ExtraMath
    {
        /// <summary>
        /// Returns the largest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The largest number</returns>
        public static T Max<T>(params T[] nums)
            // Can safely suppress null because k is less than nums.Length - 1
            where T : IComparable<T> => IterativeMax(nums.AsSpan())!;

        /// <summary>
        /// Returns the smallest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The smallest number</returns>
        public static T Min<T>(params T[] nums)
            // Can safely suppress null because k is less than nums.Length - 1
            where T : IComparable<T> => IterativeMin(nums.AsSpan());

        internal static T IterativeMax<T>(Span<T> nums)
            where T : IComparable<T>
        {
            T max = nums[0];
            for (int i = 1; i < nums.Length; i++)
                max = Max(max, nums[i]);
            return max;
        }

        internal static T IterativeMin<T>(Span<T> nums)
            where T : IComparable<T>
        {
            T min = nums[0];
            for (int i = 1; i < nums.Length; i++)
                min = Min(min, nums[i]);
            return min;
        }

        internal static T RecursiveMax<T>(Span<T> nums)
            where T : IComparable<T>
        {
            if (nums.Length == 1) return nums[0];

            int mid = nums.Length / 2;

            T left = RecursiveMax(nums.Slice(0, mid));
            T right = RecursiveMax(nums.Slice(mid));
            return Max(left, right);
        }

        internal static T RecursiveMin<T>(Span<T> nums)
            where T : IComparable<T>
        {
            if (nums.Length == 1) return nums[0];

            int mid = nums.Length / 2;

            T left = RecursiveMin(nums.Slice(0, mid));
            T right = RecursiveMin(nums.Slice(mid));
            return Min(left, right);
        }

        /// <summary>
        /// Returns the greater of two values.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <returns>The greater value.</returns>
        public static T Max<T>(T val1, T val2)
            where T : IComparable<T> => val1.CompareTo(val2) >= 0 ? val1 : val2;

        /// <summary>
        /// Returns the least of two values.
        /// </summary>
        /// <typeparam name="T">The type of values to compare.</typeparam>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <returns>The smallest value.</returns>
        public static T Min<T>(T val1, T val2)
            where T : IComparable<T> => val1.CompareTo(val2) <= 0 ? val1 : val2;


        /// <summary>
        /// Returns the smallest power of 2 greater than <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to get next power of 2 over.</param>
        /// <returns>The smallest power of 2 greater than <paramref name="value"/>.</returns>
#if NET6_0_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int RoundUpPow2(int value)
        {
            return (int)BitOperations.RoundUpToPowerOf2((uint)value);
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundUpPow2(int value)
        {
            if (value < 0) return 0;
            --value;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            return value + 1;
        }
#endif

        /// <summary>
        /// Returns the floor of <paramref name="value"/> log base 2.
        /// </summary>
        /// <param name="value">The value to obtain the logarithm of.</param>
        /// <returns>Log base of <paramref name="value"/>.</returns>
#if NET6_0_OR_GREATER
        [Obsolete($"Obsolute in .NET 6+. Use {nameof(BitOperations.Log2)}")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(int value)
        {
            return BitOperations.Log2((uint)value);
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Log2(int value)
        {
            return (int)((BitConverter.DoubleToInt64Bits(value) >> 52) + 1) & 0xFF;
        }
#endif
    }
}
