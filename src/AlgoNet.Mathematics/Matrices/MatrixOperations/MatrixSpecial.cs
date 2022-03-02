// Adam Dernis © 2022

using System.Runtime.CompilerServices;

namespace AlgoNet.Mathematics.Matrices
{
    public static partial class MatrixOperations
    {
        /// <summary>
        /// Reduces a matrix to row echelon form.
        /// </summary>
        /// <param name="matrix">The matrix to reduce.</param>
        /// <returns>The matrix in row echelon form.</returns>
        public static Matrix RowEchelon(Matrix matrix)
        {
            int col = 0;
            int r = 0;
            Unsafe.SkipInit(out double co);

            for (int row = 0; row < matrix.Height - 1; row++)
            {
                // Find next non-zero column
                // and the row of the first non-zero value in it
                for (; col < matrix.Width; col++)
                    if (!IsZeroColumn(matrix.AsSpan2D().GetColumn(col), r, out r)) break;

                // Done
                if (col == matrix.Width) return matrix;

                // Put row with leading 1 in col at row i
                if (r != row) SwapRows(matrix, r, row);
                Row rowi = matrix.AsSpan2D().GetRow(row);
                co = rowi[col];
                if (co != 1) Divide(rowi, co);

                // Put only zeros beneath rowi in the active column
                for (int j = r+1; j < matrix.Height; j++)
                {
                    Row rowj = matrix.AsSpan2D().GetRow(j);
                    co = -rowj[col];
                    if (co != 0)
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
        public static Matrix ReducedRowEchelon(Matrix matrix)
        {
            // Put matrix in row echelon form.
            RowEchelon(matrix);

            // Start at column 1 because column 0 is guarenteed complete
            for (int i = 1; i < matrix.Width; i++)
            {
                // Find the first non-zero column
                Unsafe.SkipInit(out int r);
                ReadOnlyColumn coli = matrix.AsSpan2D().GetColumn(i);
                for (r = coli.Length - 1; r >= 0; r--)
                    if (coli[r] != 0) break;

                // Put only zeros above rowr in the active column
                ReadOnlyRow rowr = matrix.AsSpan2D().GetRow(r);
                for (int j = r-1; j >= 0; j--)
                {
                    Row rowj = matrix.AsSpan2D().GetRow(j);
                    double co = -rowj[i];
                    if (co != 0)
                        Add(rowj, rowr, co);
                }
            }

            return matrix;
        }
    }
}
