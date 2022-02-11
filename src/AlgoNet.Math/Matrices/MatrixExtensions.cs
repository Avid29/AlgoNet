// Adam Dernis © 2022

using MO = AlgoNet.Mathematics.Matrices.MatrixOperations;

namespace AlgoNet.Mathematics.Matrices
{
    public static class MatrixExtensions
    {
        public static Matrix Add(this Matrix a, Matrix b)
        {
            return MO.Add(a, b, a);
        }

        public static Matrix Subtract(this Matrix a, Matrix b)
        {
            return MO.Subtract(a, b, a);
        }
    }
}
