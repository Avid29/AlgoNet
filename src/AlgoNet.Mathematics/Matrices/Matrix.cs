// Adam Dernis © 2022

using Microsoft.Toolkit.HighPerformance;
using System.Numerics;

namespace AlgoNet.Mathematics.Matrices
{
    public ref struct Matrix
    {
        private Span2D<double> _span;

        public Matrix(Span2D<double> span)
        {
            _span = span;
        }

        public Matrix(double[,] span) : this(span.AsSpan2D())
        { }

        /// <inheritdoc cref="Width"/>
        public int N => _span.Width;

        /// <inheritdoc cref="Height"/>
        public int M => _span.Height;

        /// <summary>
        /// Gets the width of the of the Matrix.
        /// </summary>
        public int Width => _span.Width;

        /// <summary>
        /// Gets the height of the Matrix.
        /// </summary>
        public int Height => _span.Height;

        /// <summary>
        /// Gets the element at the specified indices.
        /// </summary>
        /// <param name="i">The target row.</param>
        /// <param name="j">The target column.</param>
        /// <returns>A reference to the element at the specified indices.</returns>
        public ref double this[int i, int j] => ref _span[i, j];

        /// <summary>
        /// Creates an identity matrix of size NxN.
        /// </summary>
        /// <param name="n">The size of the matrix.</param>
        /// <returns>An NxN identity matrix.</returns>
        public static Matrix GetIdentityMatrix(int n)
        {
            Matrix result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                result[i, i] = 1;
            }
            return result;
        }

        public Span2D<double> AsSpan2D()
        {
            return _span;
        }

        public double[,] ToArray()
        {
            return _span.ToArray();
        }

        /// <summary>
        /// Implicitly converts a given 2D array into a <see cref="Matrix"/> instance.
        /// </summary>
        /// <param name="span">The input 2D array to convert.</param>
        public static implicit operator Matrix(double[,] span) => new Matrix(span);

        /// <summary>
        /// Implicitly converts a given <see cref="Span2D{double}"/> into a <see cref="Matrix"/> instance.
        /// </summary>
        /// <param name="span">The input <see cref="Span2D{double}"/> to convert.</param>
        public static implicit operator Matrix(Span2D<double> span) => new Matrix(span);

        /// <summary>
        /// Explictily converts a given <see cref="Matrix3x2"/> into a <see cref="Matrix"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix3x2"/> to convert.</param>
        public static explicit operator Matrix(Matrix3x2 matrix)
        {
            return new double[,]
            {
                { matrix.M11, matrix.M12 },
                { matrix.M21, matrix.M22 },
                { matrix.M31, matrix.M32 },
            };
        }

        /// <summary>
        /// Explictily converts a given <see cref="Matrix4x4"/> into a <see cref="Matrix"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix4x4"/> to convert.</param>
        public static explicit operator Matrix(Matrix4x4 matrix)
        {
            return new double[,]
            {
                { matrix.M11, matrix.M12, matrix.M13, matrix.M14 },
                { matrix.M21, matrix.M22, matrix.M23, matrix.M24 },
                { matrix.M31, matrix.M32, matrix.M33, matrix.M34 },
                { matrix.M41, matrix.M42, matrix.M43, matrix.M44 },
            };
        }


        /// <summary>
        /// Implicitly converts a given <see cref="Matrix"/> into a <see cref="Span2D{double}"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix"/> to convert.</param>
        public static implicit operator Span2D<double>(Matrix matrix) => matrix.AsSpan2D();

        /// <summary>
        /// Explictily converts a given <see cref="Matrix"/> into a 2D array.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix"/> to convert.</param>
        public static explicit operator double[,](Matrix matrix) => matrix.ToArray();

        /// <summary>
        /// Explictily converts a given <see cref="Matrix"/> into a <see cref="Matrix3x2"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix"/> to convert.</param>
        public static explicit operator Matrix3x2(Matrix matrix)
        {
            return new Matrix3x2(
                (float)matrix[0, 0], (float)matrix[0, 1],
                (float)matrix[1, 0], (float)matrix[1, 1],
                (float)matrix[2, 0], (float)matrix[2, 1]);
        }

        /// <summary>
        /// Explictily converts a given <see cref="Matrix"/> into a <see cref="Matrix4x4"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix"/> to convert.</param>
        public static explicit operator Matrix4x4(Matrix matrix)
        {
            return new Matrix4x4(
                (float)matrix[0, 0], (float)matrix[0, 1], (float)matrix[0, 2], (float)matrix[0, 3],
                (float)matrix[1, 0], (float)matrix[1, 1], (float)matrix[1, 2], (float)matrix[1, 3],
                (float)matrix[2, 0], (float)matrix[2, 1], (float)matrix[2, 2], (float)matrix[2, 3],
                (float)matrix[3, 0], (float)matrix[3, 1], (float)matrix[3, 2], (float)matrix[3, 3]);
        }

        public static Matrix operator +(Matrix a, Matrix b) => MatrixOperations.Add(a, b);

        public static Matrix operator -(Matrix a, Matrix b) => MatrixOperations.Subtract(a, b);

        public static Matrix operator *(Matrix a, Matrix b) => MatrixOperations.Multiply(a, b);
    }
}
