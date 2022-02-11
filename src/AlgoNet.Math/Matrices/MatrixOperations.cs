// Adam Dernis © 2022

using Microsoft.Toolkit.Diagnostics;

namespace AlgoNet.Mathematics.Matrices
{
    public class MatrixOperations
    {
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            if (a.Width != b.Height) ThrowHelper.ThrowArgumentException($"Dimensions of a and b don't match.");

            Matrix r = new double[a.Height, b.Width];

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

        internal static Matrix Add(Matrix a, Matrix b)
        {
            Matrix target = new double[a.Height, a.Width];
            Add(a, b, target);
            return target;
        }

        internal static Matrix Add(Matrix a, Matrix b, Matrix target)
        {
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    target[i, j] = a[i, j] + b[i, j];

            return target;
        }

        internal static Matrix Subtract(Matrix a, Matrix b)
        {
            Matrix target = new double[a.Height, a.Width];
            Subtract(a, b, target);
            return target;
        }

        internal static Matrix Subtract(Matrix a, Matrix b, Matrix target)
        {
            for (int i = 0; i < a.Height; i++)
                for (int j = 0; j < a.Width; j++)
                    target[i, j] = a[i, j] - b[i, j];

            return target;
        }
    }
}
