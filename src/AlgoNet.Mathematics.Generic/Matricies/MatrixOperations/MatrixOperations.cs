// Adam Dernis © 2022

using Microsoft.Toolkit.Diagnostics;
using System;

namespace AlgoNet.Mathematics.Generic.Matrices
{
    /// <summary>
    /// A static class containing operations for <see cref="Matrix{T}"/> as a matrix.
    /// </summary>
    public static partial class MatrixOperations
    {
        /// <inheritdoc cref="Add{T}(Matrix{T}, Matrix{T}, Matrix{T}"/>
        public static Matrix<T> Add<T>(Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            Matrix<T> target = new T[a.Height, a.Width];
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
        public static Matrix<T> Add<T>(Matrix<T> a, Matrix<T> b, Matrix<T> target)
            where T : INumber<T>
        {
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    target[i, j] = a[i, j] + b[i, j];

            return target;
        }

        /// <inheritdoc cref="Subtract{T}(Matrix{T}, Matrix{T}, Matrix{T}"/>
        public static Matrix<T> Subtract<T>(Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            Matrix<T> target = new T[a.Height, a.Width];
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
        public static Matrix<T> Subtract<T>(Matrix<T> a, Matrix<T> b, Matrix<T> target)
            where T : INumber<T>
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
        public static Matrix<T> Multiply<T>(Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            if (a.Width != b.Height) ThrowHelper.ThrowArgumentException($"Dimensions of a and b don't match.");

            Matrix<T> r = new T[a.Height, b.Width];

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
