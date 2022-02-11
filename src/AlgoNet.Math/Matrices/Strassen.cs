// Adam Dernis © 2022

using Microsoft.Toolkit.Diagnostics;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AlgoNet.Mathematics.Matrices
{
    internal static class Strassen
    {
        // Reference: http://www.ivank.net/blogspot/matrix_cs/Matrix.cs

        /// <summary>
        /// Multiplies two matricies with the Strassen method.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>The product of A and B.</returns>
        internal static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Width != b.Height) ThrowHelper.ThrowArgumentException($"Dimensions of a and b don't match.");

            int maxSize = ExtraMath.Max(a.Width, a.Height, b.Width, b.Height);
            int size = RoundUpPow2(maxSize);
            int n = Log2(size);
            int h = size / 2;
            
            throw new NotImplementedException();
        }

        internal static Matrix NaiveMultiply(Matrix a, Matrix b)
        {
            if (a.Width != b.Height) ThrowHelper.ThrowArgumentException($"Dimensions of a and b don't match.");

            Matrix r = new double[a.Height, b.Width];

            // Rows
            for (int i = 0; i < a.Height; i++)
            {
                // Columns
                for (int j = 0; j < b.Width; j++)
                {
                    // Sum
                    for (int k = 0; k < a.Width; k++)
                    {
                        r[i,j] += a[i, k] * b[k, j];
                    }
                }
            }

            return r;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int RoundUpPow2(int x)
        {
#if NET6_0_OR_GREATER
            return (int)BitOperations.RoundUpToPowerOf2((uint)x);
#else
            if (x < 0) return 0;
            --x;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Log2(int x)
        {
#if NET6_0_OR_GREATER
            return BitOperations.Log2((uint)x);
#else
            return (int)((BitConverter.DoubleToInt64Bits(x) >> 52) + 1) & 0xFF;
#endif
        }
    }
}
