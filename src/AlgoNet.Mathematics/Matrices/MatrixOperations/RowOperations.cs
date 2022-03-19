// Adam Dernis © 2022

namespace AlgoNet.Mathematics.Matrices
{
    public static partial class MatrixOperations
    {
        internal static void SwapRows(Matrix matrix, int row1, int row2)
        {
            SwapRows(matrix.AsSpan2D().GetRow(row1), matrix.AsSpan2D().GetRow(row2));
        }

        internal static void SwapRows(Row row1, Row row2)
        {
            for (int i = 0; i < row1.Length; i++)
            {
                double swap = row1[i];
                row1[i] = row2[i];
                row2[i] = swap;
            }
        }

        internal static void Add(Row row1, ReadOnlyRow row2, double coefficient)
        {
            for (int i = 0; i < row1.Length; i++)
                row1[i] += coefficient * row2[i];
        }

        internal static void Divide(Row row, double value)
        {
            for (int i = 0; i < row.Length; i++)
                row[i] /= value;
        }

        /// <inheritdoc cref="IsZeroColumn(Microsoft.Toolkit.HighPerformance.Enumerables.ReadOnlyRefEnumerable{double}, int, out int)"/>
        public static bool IsZeroColumn(ReadOnlyColumn column, out int row) => IsZeroColumn(column, 0, out row);

        /// <summary>
        /// Check whether or not a column contains only zeros.
        /// </summary>
        /// <param name="column">The columnn to check.</param>
        /// <param name="start">The first row to check from.</param>
        /// <param name="row">The row where the first non-zero value was found, or -1 if zero column.</param>
        /// <returns>True if <paramref name="column"/> contains only zeros.</returns>
        internal static bool IsZeroColumn(ReadOnlyColumn column, int start, out int row)
        {
            for (row = start; row < column.Length; row++)
                if (column[row] != 0) return false;

            row = -1;
            return true;
        }
    }
}
