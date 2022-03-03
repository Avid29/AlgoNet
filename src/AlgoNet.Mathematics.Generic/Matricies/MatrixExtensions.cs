// Adam Dernis © 2022

using System;
using MO = AlgoNet.Mathematics.Generic.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Generic.Matrices
{
    /// <summary>
    /// A static class containing extensions methods for <see cref="Matrix{T}"/> as a matrix.
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        /// Adds matrix B to matrix A.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>A+B as Matrix A.</returns>
        public static Matrix<T> Add<T>(this Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            return MO.Add(a, b, a);
        }

        /// <summary>
        /// Subtracts matrix B from matrix A.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>A-B as Matrix A.</returns>
        public static Matrix<T> Subtract<T>(this Matrix<T> a, Matrix<T> b)
            where T : INumber<T>
        {
            return MO.Subtract(a, b, a);
        }
    }
}
