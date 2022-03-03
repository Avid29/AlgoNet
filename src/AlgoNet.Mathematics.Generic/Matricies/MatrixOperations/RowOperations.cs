// Adam Dernis © 2022

using Microsoft.Toolkit.HighPerformance.Enumerables;
using System;

namespace AlgoNet.Mathematics.Generic.Matrices
{
    public static partial class MatrixOperations
    {
        internal static void SwapRows<T>(Matrix<T> matrix, int row1, int row2)
            where T : INumber<T>
        {
            SwapRows(matrix.AsSpan2D().GetRow(row1), matrix.AsSpan2D().GetRow(row2));
        }

        internal static void SwapRows<T>(RefEnumerable<T> row1, RefEnumerable<T> row2)
            where T : INumber<T>
        {
            for (int i = 0; i < row1.Length; i++)
            {
                T swap = row1[i];
                row1[i] = row2[i];
                row2[i] = swap;
            }
        }

        internal static void Add<T>(RefEnumerable<T> row1, ReadOnlyRefEnumerable<T> row2, T coefficient)
            where T : INumber<T>
        {
            for (int i = 0; i < row1.Length; i++)
                row1[i] += coefficient * row2[i];
        }

        internal static void Divide<T>(RefEnumerable<T> row, T value)
            where T : INumber<T>
        {
            for (int i = 0; i < row.Length; i++)
                row[i] /= value;
        }

        /// <inheritdoc cref="IsZeroColumn{T}(ReadOnlyRefEnumerable{T}, int, out int)"/>
        public static bool IsZeroColumn<T>(ReadOnlyRefEnumerable<T> column, out int row) where T : INumber<T> => IsZeroColumn(column, 0, out row);

        /// <summary>
        /// Check whether or not a column contains only zeros.
        /// </summary>
        /// <param name="column">The columnn to check.</param>
        /// <param name="start">The first row to check from.</param>
        /// <param name="row">The row where the first non-zero value was found, or -1 if zero column.</param>
        /// <returns>True if <paramref name="column"/> contains only zeros.</returns>
        internal static bool IsZeroColumn<T>(ReadOnlyRefEnumerable<T> column, int start, out int row)
            where T : INumber<T>
        {
            for (row = start; row < column.Length; row++)
                if (column[row] != T.Zero) return false;

            row = -1;
            return true;
        }
    }
}
