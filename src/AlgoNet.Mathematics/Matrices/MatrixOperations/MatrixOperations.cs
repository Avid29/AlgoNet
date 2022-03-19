// Adam Dernis © 2022

using Microsoft.Toolkit.Diagnostics;

namespace AlgoNet.Mathematics.Matrices
{
    /// <summary>
    /// A static class containing operations for <see cref="Matrix"/> as a matrix.
    /// </summary>
    public static partial class MatrixOperations
    {
        /// <inheritdoc cref="Add(Matrix, Matrix, Matrix"/>
        public static Matrix Add(Matrix a, Matrix b)
        {
            Matrix target = new double[a.Height, a.Width];
            Add(a, b, target);
            return target;
        }

        /// <summary>
        /// Adds matrix A and matrix B.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <param name="target">Target matrix where A+B is written.</param>
        /// <returns><paramref name="target"/> as A+B.</returns>
        public static Matrix Add(Matrix a, Matrix b, Matrix target)
        {
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    target[i, j] = a[i, j] + b[i, j];

            return target;
        }

        /// <inheritdoc cref="Subtract(Matrix, Matrix, Matrix"/>
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            Matrix target = new double[a.Height, a.Width];
            Subtract(a, b, target);
            return target;
        }

        /// <summary>
        /// Subtracts matrix A from matrix B.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <param name="target">Target matrix where A-B is written.</param>
        /// <returns><paramref name="target"/> as A-B.</returns>
        public static Matrix Subtract(Matrix a, Matrix b, Matrix target)
        {
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    target[i, j] = a[i, j] - b[i, j];

            return target;
        }

        /// <summary>
        /// Multiplies matrix A by matrix B.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>Matrix AB.</returns>
        public static Matrix Multiply(Matrix a, Matrix b)
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
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return r;
        }
    }
}
