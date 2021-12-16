// Adam Dernis © 2021

using AlgoNet.Clustering.Kernels;

namespace AlgoNet.Clustering.Methods.MeanShift
{
    /// <summary>
    /// Configuration info for MeanShift clustering to run.
    /// </summary>
    /// <typeparam name="T">The type of points used to cluster.</typeparam>
    /// <typeparam name="TShape">The shape to use on the points to cluster.</typeparam>
    /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
    public struct MSConfig<T, TShape, TKernel>
        where T : unmanaged
        where TShape : struct, IGeometricPoint<T>
        where TKernel : struct, IKernel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSConfig{T, TShape, TKernel}"/> struct.
        /// </summary>
        /// <param name="kernel"></param>
        public MSConfig(TKernel kernel)
        {
            Kernel = kernel;
        }

        /// <summary>
        /// Gets the <see cref="TKernel"/> used to weight distances of points.
        /// </summary>
        public TKernel Kernel { get; }
    }
}
