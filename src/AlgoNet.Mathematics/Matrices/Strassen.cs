// Adam Dernis © 2022

using System;
using System.Numerics;
using MO = AlgoNet.Mathematics.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Matrices
{
    /// <summary>
    /// A static class containing methods for Strassen multiplication
    /// </summary>
    internal static class Strassen
    {
        /// <summary>
        /// Multiplies two matricies with the Strassen method.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>The product of A and B.</returns>
        internal static Matrix Multiply(Matrix a, Matrix b)
        {
            // TODO: Custom exceptions
            if (a.Width != b.Height) throw new Exception("Matrix A can not be multiplied by Matrix B");

            int finalWidth = b.Width;
            int finalHeight = a.Height;

            // Expand to N^2 x N^2 identity
            a = IdentityExpand2(a);
            b = IdentityExpand2(b);
            
            Matrix result = MultiplyNN(a, b);
            return result.AsSpan2D().Slice(0, 0, finalHeight, finalWidth);
        }

        private static Matrix IdentityExpand2(Matrix matrix)
        {
            int max = ExtraMath.Max(matrix.Width, matrix.Height);
            max = ExtraMath.RoundUpPow2(max);
            Matrix result = Matrix.GetIdentityMatrix(max);
            matrix.AsSpan2D().CopyTo(result);
            return result;
        }

        private static Matrix MultiplyNN(Matrix a, Matrix b)
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

            Matrix a11 = a.AsSpan2D().Slice(0, 0, k, k);
            Matrix a12 = a.AsSpan2D().Slice(0, k, k, k);
            Matrix a21 = a.AsSpan2D().Slice(k, 0, k, k);
            Matrix a22 = a.AsSpan2D().Slice(k, k, k, k);
            Matrix b11 = b.AsSpan2D().Slice(0, 0, k, k);
            Matrix b12 = b.AsSpan2D().Slice(0, k, k, k);
            Matrix b21 = b.AsSpan2D().Slice(k, 0, k, k);
            Matrix b22 = b.AsSpan2D().Slice(k, k, k, k);

            // P1 = A11 * (B12 - B22)
            Matrix p1 = MultiplyNN(a11, MO.Subtract(b12, b22));
            
            // P2 = (A11 + A12) * B22
            Matrix p2 = MultiplyNN(MO.Add(a11, a12), b22);
            
            // P3 = (A21 + A22) * B11
            Matrix p3 = MultiplyNN(MO.Add(a21, a22), b11);
            
            // P4 = A22 * (B21 - B11)
            Matrix p4 = MultiplyNN(a22, MO.Subtract(b21, b11));
            
            // P5 = (A11 + A22) * (B11 + B22)
            Matrix p5 = MultiplyNN(MO.Add(a11, a22), MO.Add(b11, b22));
            
            // P6 = (A12 - A22) * (B21 + B22)
            Matrix p6 = MultiplyNN(MO.Subtract(a12, a22), MO.Add(b21, b22));
            
            // P7 = (A11 - A21) * (B11 + B12)
            Matrix p7 = MultiplyNN(MO.Subtract(a11, a21), MO.Add(b11, b12));

            c = new double[n, n];
            Matrix c11 = c.AsSpan2D().Slice(0, 0, k, k);
            Matrix c12 = c.AsSpan2D().Slice(0, k, k, k);
            Matrix c21 = c.AsSpan2D().Slice(k, 0, k, k);
            Matrix c22 = c.AsSpan2D().Slice(k, k, k, k);

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
    }
}
