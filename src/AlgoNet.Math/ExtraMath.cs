// Adam Dernis © 2022

using System;

namespace AlgoNet.Mathematics
{
    /// <summary>
    /// Extra math operations
    /// </summary>
    public static class ExtraMath
    {
        #region double
        /// <summary>
        /// Returns the largest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The largest number</returns>
        public static double Max(params double[] nums) => Max(nums.AsSpan());

        private static double Max(Span<double> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);

            int mid = nums.Length / 2;

            double left = Max(nums.Slice(0, mid));
            double right = Max(nums.Slice(mid));
            return Math.Max(left, right);
        }

        /// <summary>
        /// Returns the smallest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The smallest number</returns>
        public static double Min(params double[] nums) => Min(nums.AsSpan());

        private static double Min(Span<double> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Min(nums[0], nums[1]);

            int mid = nums.Length / 2;

            double left = Min(nums.Slice(0, mid));
            double right = Min(nums.Slice(mid));
            return Math.Min(left, right);
        }
        #endregion

        #region int

        /// <summary>
        /// Returns the largest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The largest number</returns>
        public static int Max(params int[] nums) => Max(nums.AsSpan());

        private static int Max(Span<int> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Max(nums[0], nums[1]);

            int mid = nums.Length / 2;

            int left = Max(nums.Slice(0, mid));
            int right = Max(nums.Slice(mid));
            return Math.Max(left, right);
        }

        /// <summary>
        /// Returns the smallest number.
        /// </summary>
        /// <param name="nums">The numbers to pull from.</param>
        /// <returns>The smallest number</returns>
        public static int Min(params int[] nums) => Min(nums.AsSpan());

        private static int Min(Span<int> nums)
        {
            if (nums.Length == 1) return nums[0];
            if (nums.Length == 2) return Math.Min(nums[0], nums[1]);

            int mid = nums.Length / 2;

            int left = Min(nums.Slice(0, mid));
            int right = Min(nums.Slice(mid));
            return Math.Min(left, right);
        }

        #endregion
    }
}
