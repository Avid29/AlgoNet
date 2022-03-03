// Adam Dernis © 2022

using Microsoft.Toolkit.HighPerformance;
using System;

namespace AlgoNet.Mathematics.Generic.Matrices
{
    public ref struct Matrix<T>
        where T : INumber<T>
    {
        private Span2D<T> _span;

        public Matrix(Span2D<T> span)
        {
            _span = span;
        }

        public Matrix(T[,] span) : this(span.AsSpan2D())
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
        public ref T this[int i, int j] => ref _span[i, j];

        /// <summary>
        /// Creates an identity matrix of size NxN.
        /// </summary>
        /// <param name="n">The size of the matrix.</param>
        /// <returns>An NxN identity matrix.</returns>
        public static Matrix<T> GetIdentityMatrix(int n)
        {
            Matrix<T> result = new T[n, n];
            for (int i = 0; i < n; i++)
            {
                result[i, i] = T.One;
            }
            return result;
        }

        public Span2D<T> AsSpan2D()
        {
            return _span;
        }

        public T[,] ToArray()
        {
            return _span.ToArray();
        }

        /// <summary>
        /// Implicitly converts a given 2D array into a <see cref="Matrix{T}"/> instance.
        /// </summary>
        /// <param name="span">The input 2D array to convert.</param>
        public static implicit operator Matrix<T>(T[,] span) => new Matrix<T>(span);

        /// <summary>
        /// Implicitly converts a given <see cref="Span2D{T}"/> into a <see cref="Matrix{T}"/> instance.
        /// </summary>
        /// <param name="span">The input <see cref="Span2D{T}"/> to convert.</param>
        public static implicit operator Matrix<T>(Span2D<T> span) => new Matrix<T>(span);


        /// <summary>
        /// Implicitly converts a given <see cref="Matrix{T}"/> into a <see cref="Span2D{T}"/> instance.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix{T}"/> to convert.</param>
        public static implicit operator Span2D<T>(Matrix<T> matrix) => matrix.AsSpan2D();

        /// <summary>
        /// Explictily converts a given <see cref="Matrix{T}"/> into a 2D array.
        /// </summary>
        /// <param name="matrix">The input <see cref="Matrix{T}"/> to convert.</param>
        public static explicit operator T[,](Matrix<T> matrix) => matrix.ToArray();

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b) => MatrixOperations.Add(a, b);

        public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b) => MatrixOperations.Subtract(a, b);

        public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b) => MatrixOperations.Multiply(a, b);
    }
}
