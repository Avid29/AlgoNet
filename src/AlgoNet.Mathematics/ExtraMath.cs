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
            where T : IComparable<T> => QuickSelect.Select(nums.AsSpan(), nums.Length - 1)!;

        /// <summary>
        /// Returns the smallest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The smallest number</returns>
        public static T Min<T>(params T[] nums)
            // Can safely suppress null because k is less than nums.Length - 1
            where T : IComparable<T> => QuickSelect.Select(nums.AsSpan(), 0)!;

        #region double

        internal static double NaiveMax(Span<double> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);

            int mid = nums.Length / 2;

            double left = NaiveMax(nums.Slice(0, mid));
            double right = NaiveMax(nums.Slice(mid));
            return Math.Max(left, right);
        }

        internal static double NaiveMin(Span<double> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Min(nums[0], nums[1]);

            int mid = nums.Length / 2;

            double left = NaiveMin(nums.Slice(0, mid));
            double right = NaiveMin(nums.Slice(mid));
            return Math.Min(left, right);
        }
        #endregion

        #region int

        internal static int NaiveMax(Span<int> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);

            int mid = nums.Length / 2;

            int left = NaiveMax(nums.Slice(0, mid));
            int right = NaiveMax(nums.Slice(mid));
            return Math.Max(left, right);
        }

        internal static int NaiveMin(Span<int> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Min(nums[0], nums[1]);

            int mid = nums.Length / 2;

            int left = NaiveMin(nums.Slice(0, mid));
            int right = NaiveMin(nums.Slice(mid));
            return Math.Min(left, right);
        }

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
        #endregion
    }
}
