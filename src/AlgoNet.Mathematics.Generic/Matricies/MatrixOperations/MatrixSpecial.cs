// Adam Dernis © 2022

using Microsoft.Toolkit.HighPerformance.Enumerables;
using System;
using System.Runtime.CompilerServices;

namespace AlgoNet.Mathematics.Generic.Matrices
{
    public static partial class MatrixOperations
    {
        /// <summary>
        /// Reduces a matrix to row echelon form.
        /// </summary>
        /// <param name="matrix">The matrix to reduce.</param>
        /// <returns>The matrix in row echelon form.</returns>
        public static Matrix<T> RowEchelon<T>(Matrix<T> matrix)
            where T : INumber<T>
        {
            int col = 0;
            int r = 0;
            Unsafe.SkipInit(out T co);

            for (int row = 0; row < matrix.Height - 1; row++)
            {
                // Find next non-zero column
                // and the row of the first non-zero value in it
                for (; col < matrix.Width; col++)
                    if (!IsZeroColumn<T>(matrix.AsSpan2D().GetColumn(col), r, out r)) break;

                // Done
                if (col == matrix.Width) return matrix;

                // Put row with leading 1 in col at row i
                if (r != row) SwapRows(matrix, r, row);
                RefEnumerable<T> rowi = matrix.AsSpan2D().GetRow(row);
                co = rowi[col];
                if (co != T.One) Divide(rowi, co);

                // Put only zeros beneath rowi in the active column
                for (int j = r+1; j < matrix.Height; j++)
                {
                    RefEnumerable<T> rowj = matrix.AsSpan2D().GetRow(j);
                    co = -rowj[col];
                    if (co != T.Zero)
                        Add(rowj, rowi, co);
                }

                col++;
                r++;
            }

            return matrix;
        }

        /// <summary>
        /// Reduces a matrix to reduced row echelon form.
        /// </summary>
        /// <param name="matrix">The matrix to reduce.</param>
        /// <returns>The matrix in reduced row echelon form.</returns>
        public static Matrix<T> ReducedRowEchelon<T>(Matrix<T> matrix)
            where T : INumber<T>
        {
            // Put matrix in row echelon form.
            RowEchelon(matrix);

            // Start at column 1 because column 0 is guarenteed complete
            for (int i = 1; i < matrix.Width; i++)
            {
                // Find the first non-zero column
                Unsafe.SkipInit(out int r);
                ReadOnlyRefEnumerable<T> coli = matrix.AsSpan2D().GetColumn(i);
                for (r = coli.Length - 1; r >= 0; r--)
                    if (coli[r] != T.Zero) break;

                // Put only zeros above rowr in the active column
                ReadOnlyRefEnumerable<T> rowr = matrix.AsSpan2D().GetRow(r);
                for (int j = r-1; j >= 0; j--)
                {
                    RefEnumerable<T> rowj = matrix.AsSpan2D().GetRow(j);
                    T co = -rowj[i];
                    if (co != T.Zero)
                        Add(rowj, rowr, co);
                }
            }

            return matrix;
        }
    }
}
