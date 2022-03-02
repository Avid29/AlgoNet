using ComputeSharp;
using AlgoNet.Mathematics.GPU.Matricies.MatrixOperations.Shaders;
using AlgoNet.Mathematics.Matrices;
using Microsoft.Toolkit.HighPerformance;

namespace AlgoNet.Mathematics.GPU.Matricies.MatrixOperations
{
    /// <summary>
    /// A static class containing operations for <see cref="Matrix"/> as a matrix.
    /// </summary>
    public static class MatrixOperations
    {
        /// <inheritdoc cref="Multiply(Matrix, Matrix, Matrix, GraphicsDevice"/>
        public static Matrix Multiply(Matrix a, Matrix b) => Multiply(a, b, GraphicsDevice.Default);

        /// <summary>
        /// Multiplies matrix A by matrix B.
        /// </summary>
        /// <param name="a">Matrix A.</param>
        /// <param name="b">Matrix B.</param>
        /// <param name="device">The <see cref="GraphicsDevice"/> to run on.</param>
        /// <returns>Matrix AB.</returns>
        public static Matrix Multiply(Matrix a, Matrix b, GraphicsDevice device)
        {
            ReadWriteBuffer<double> targetBuffer = device.AllocateReadWriteBuffer<double>(a.Height * b.Width);
            ReadOnlyBuffer<double> aBuffer = device.AllocateReadOnlyBuffer<double>(a);
            ReadOnlyBuffer<double> bBuffer = device.AllocateReadOnlyBuffer<double>(b);

            device.For(a.Height, b.Width, new MatrixMultiplicationShader(aBuffer, bBuffer, targetBuffer, b.Width));

            Matrix target = new double[a.Height, b.Width];

            targetBuffer.CopyTo(target);
            return target;
        }
    }
}
