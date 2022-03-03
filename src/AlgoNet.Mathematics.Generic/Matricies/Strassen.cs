// Adam Dernis © 2022

using System;
using MO = AlgoNet.Mathematics.Generic.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Generic.Matrices
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
        internal static Matrix<T> Multiply<T>(Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            // TODO: Custom exceptions
            if (a.Width != b.Height) throw new Exception("Matrix A can not be multiplied by Matrix B");

            int finalWidth = b.Width;
            int finalHeight = a.Height;

            // Expand to N^2 x N^2 identity
            a = IdentityExpand2(a);
            b = IdentityExpand2(b);
            
            Matrix<T> result = MultiplyNN(a, b);
            return result.AsSpan2D().Slice(0, 0, finalHeight, finalWidth);
        }

        private static Matrix<T> IdentityExpand2<T>(Matrix<T> matrix)
            where T : INumber<T>
        {
            int max = ExtraMath.Max(matrix.Width, matrix.Height);
            max = Mathematics.ExtraMath.RoundUpPow2(max);
            Matrix<T> result = Matrix<T>.GetIdentityMatrix(max);
            matrix.AsSpan2D().CopyTo(result);
            return result;
        }

        private static Matrix<T> MultiplyNN<T>(Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            int n = a.Width;

            Matrix<T> c;
            if (n == 1)
            {
                c = new T[1,1];
                c[0,0] = a[0,0] * b[0,0];
                return c;
            }

            int k = n / 2;

            Matrix<T> a11 = a.AsSpan2D().Slice(0, 0, k, k);
            Matrix<T> a12 = a.AsSpan2D().Slice(0, k, k, k);
            Matrix<T> a21 = a.AsSpan2D().Slice(k, 0, k, k);
            Matrix<T> a22 = a.AsSpan2D().Slice(k, k, k, k);
            Matrix<T> b11 = b.AsSpan2D().Slice(0, 0, k, k);
            Matrix<T> b12 = b.AsSpan2D().Slice(0, k, k, k);
            Matrix<T> b21 = b.AsSpan2D().Slice(k, 0, k, k);
            Matrix<T> b22 = b.AsSpan2D().Slice(k, k, k, k);

            // P1 = A11 * (B12 - B22)
            Matrix<T> p1 = MultiplyNN(a11, MO.Subtract(b12, b22));
            
            // P2 = (A11 + A12) * B22
            Matrix<T> p2 = MultiplyNN(MO.Add(a11, a12), b22);
            
            // P3 = (A21 + A22) * B11
            Matrix<T> p3 = MultiplyNN(MO.Add(a21, a22), b11);
            
            // P4 = A22 * (B21 - B11)
            Matrix<T> p4 = MultiplyNN(a22, MO.Subtract(b21, b11));
            
            // P5 = (A11 + A22) * (B11 + B22)
            Matrix<T> p5 = MultiplyNN(MO.Add(a11, a22), MO.Add(b11, b22));
            
            // P6 = (A12 - A22) * (B21 + B22)
            Matrix<T> p6 = MultiplyNN(MO.Subtract(a12, a22), MO.Add(b21, b22));
            
            // P7 = (A11 - A21) * (B11 + B12)
            Matrix<T> p7 = MultiplyNN(MO.Subtract(a11, a21), MO.Add(b11, b12));

            c = new T[n, n];
            Matrix<T> c11 = c.AsSpan2D().Slice(0, 0, k, k);
            Matrix<T> c12 = c.AsSpan2D().Slice(0, k, k, k);
            Matrix<T> c21 = c.AsSpan2D().Slice(k, 0, k, k);
            Matrix<T> c22 = c.AsSpan2D().Slice(k, k, k, k);

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
