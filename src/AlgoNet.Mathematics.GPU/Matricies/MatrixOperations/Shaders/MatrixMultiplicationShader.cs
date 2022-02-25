// Adam Dernis © 2022

using ComputeSharp;

namespace AlgoNet.Mathematics.GPU.Matricies.MatrixOperations.Shaders
{
    /// <summary>
    /// A <see cref="IComputeShader"/> that adds two matricies.
    /// </summary>
    [AutoConstructor]
    [EmbeddedBytecode(DispatchAxis.XY)]
    public partial struct MatrixMultiplicationShader : IComputeShader
    {
        ReadOnlyBuffer<double> a;
        ReadOnlyBuffer<double> b;
        ReadWriteBuffer<double> target;
        int bWidth;

        /// <inheritdoc/>
        public void Execute()
        {
            int i = ThreadIds.X;
            int j = ThreadIds.Y;
            int width = DispatchSize.X;
            for (int k = 0; k < width; k++)
            {
                target[i * width + j] += a[i * width + k] * b[k * bWidth + j];
            }
        }
    }
}
