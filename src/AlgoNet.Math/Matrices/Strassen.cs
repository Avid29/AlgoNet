// Adam Dernis © 2022

using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MO = AlgoNet.Mathematics.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Matrices
{
    /// <summary>
    /// A static class containing methods for Strassen multiplication
    /// </summary>
    public static class Strassen
    {
        /// <summary>
        /// Multiplies two matricies with the Strassen method.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>The product of A and B.</returns>
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            // TODO: NxM multiplication
            int n = a.Width;

            Matrix c;
            if (n == 1)
            {
                c = new double[1,1];
                c[0,0] = a[0,0] * b[0,0];
                return c;
            }

            int k = n / 2;

            Matrix a11 = a.Slice(0, 0, k, k);
            Matrix a12 = a.Slice(0, k, k, k);
            Matrix a21 = a.Slice(k, 0, k, k);
            Matrix a22 = a.Slice(k, k, k, k);
            Matrix b11 = b.Slice(0, 0, k, k);
            Matrix b12 = b.Slice(0, k, k, k);
            Matrix b21 = b.Slice(k, 0, k, k);
            Matrix b22 = b.Slice(k, k, k, k);

            // P1 = A11 * (B12 + B22)
            Matrix p1 = Multiply(a11, MO.Subtract(b12, b22));
            
            // P2 = (A11 + A12) * B22
            Matrix p2 = Multiply(MO.Add(a11, a12), b22);
            
            // P3 = (A21 + A22) * B11
            Matrix p3 = Multiply(MO.Add(a21, a22), b11);
            
            // P4 = A22 * (B21 - B11)
            Matrix p4 = Multiply(a22, MO.Subtract(b21, b11));
            
            // P5 = (A11 + A22) * (B11 + B22)
            Matrix p5 = Multiply(MO.Add(a11, a22), MO.Add(b11, b22));
            
            // P6 = (A12 - A22) * (B21 + B22)
            Matrix p6 = Multiply(MO.Subtract(a12, a22), MO.Add(b21, b22));
            
            // P7 = (A11 - A21) * (B11 + B12)
            Matrix p7 = Multiply(MO.Subtract(a11, a21), MO.Add(b11, b12));

            c = new double[n, n];
            Matrix c11 = c.Slice(0, 0, k, k);
            Matrix c12 = c.Slice(0, k, k, k);
            Matrix c21 = c.Slice(k, 0, k, k);
            Matrix c22 = c.Slice(k, k, k, k);

            // C11 = P5 + P4 - P2 + P6
            MO.Add(p5, p4, c11).Subtract(p2).Add(p6);

            // C12 = P1 + P2
            MO.Add(p1, p2, c12);

            // C21 = P3 + P4
            MO.Add(p3, p4, c21);

            // C22 = P5 + P1 - P3 - P7
            MO.Add(p5, p1, c22).Subtract(p3).Subtract(p7);

            return c;
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
