// Adam Dernis © 2021

namespace AlgoNet.Clustering.Kernels
{
    /// Inorder to be a valid Kernel, it must meet two requirements.
    /// #1: The integral of K(u)du = 1
    /// #2: K(u) = K(|u|)
    /// 
    /// In otherwords,
    /// It must be normalized and symmetric

    /// <summary>
    /// An <see langword="interface"/> for a kernel distribution.
    /// </summary>
    public interface IKernel
    {
        /// <summary>
        /// Gets or sets the windows size of the kernel.
        /// </summary>
        double WindowSize { get; set; }

        /// <summary>
        /// Gets the weighted relevence of a point at sqrt(<paramref name="distanceSquared"/>) away.
        /// </summary>
        /// <param name="distanceSquared">The distance^2 of the point to be weighted.</param>
        /// <returns>The weight of a point at sqrt(<paramref name="distanceSquared"/>) away.</returns>
        double WeightDistance(double distanceSquared);
    }
}
