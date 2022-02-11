// Adam Dernis © 2022

using MO = AlgoNet.Mathematics.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Matrices
{
    /// <summary>
    /// A static class containing extensions methods for <see cref="Matrix"/> as a matrix.
    /// </summary>
    public static class MatrixExtensions
    {
        /// <summary>
        /// Adds matrix B to matrix A.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>A+B as Matrix A.</returns>
        public static Matrix Add(this Matrix a, Matrix b)
        {
            return MO.Add(a, b, a);
        }

        /// <summary>
        /// Subtracts matrix B from matrix A.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <returns>A-B as Matrix A.</returns>
        public static Matrix Subtract(this Matrix a, Matrix b)
        {
            return MO.Subtract(a, b, a);
        }
    }
}
