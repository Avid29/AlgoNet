// Adam Dernis © 2022

namespace AlgoNet.Mathematics.Matrices
{
    public static class MatrixExtensions
    {
        public static Matrix Multiply(this Matrix a, Matrix b)
        {
            return Strassen.NaiveMultiply(a, b);
        }
    }
}
