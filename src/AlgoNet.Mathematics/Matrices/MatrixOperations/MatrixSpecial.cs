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

            for (int i = 0; i < matrix.Height - 1; i++)
            {
                // Find next non-zero column.
                for (; col < matrix.Width; col++)
                    if (!IsZeroColumn(matrix.GetColumn(col), r, out r)) break;

                // Done
                if (col == matrix.Width) return matrix;

                // Put row with leading 1 in col at row i
                if (r != i) SwapRows(matrix, r, i);
                Row rowi = matrix.GetRow(i);
                co = rowi[col];
                if (co != 1) Divide(rowi, co);

                // Put only zeros beneath rowi in the active column
                for (int j = r+1; j < matrix.Height; j++)
                {
                    Row rowj = matrix.GetRow(j);
                    co = -rowj[col];
                    if (co != 0)
                        Add(rowj, rowi, co);
                }

                col++;
                r++;
            }

            return matrix;
        }
    }
}
