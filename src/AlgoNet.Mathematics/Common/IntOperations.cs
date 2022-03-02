// Adam Dernis © 2022

using System;
using System.Numerics;
using System.Runtime.CompilerServices;


namespace AlgoNet.Mathematics
{
    public static partial class ExtraMath
    {
        /// <summary>
        /// Returns the smallest power of 2 greater than <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to get next power of 2 over.</param>
        /// <returns>The smallest power of 2 greater than <paramref name="value"/>.</returns>
#if NET6_0_OR_GREATER
        [Obsolete($"Obsolute in .NET 6+. Use {nameof(BitOperations.RoundUpToPowerOf2)}")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundUpPow2(int value)
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
