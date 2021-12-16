// Adam Dernis © 2021

namespace AlgoNet.Clustering.IO.Interfaces
{
    /// <summary>
    /// An interface for Clusters that output with a Centroid.
    /// </summary>
    /// <typeparam name="T">The type of data in the cluster.</typeparam>
    public interface ICentroidCluster<T>
        where T : unmanaged
    {
        /// <summary>
        /// Gets the center point of all points in the <see cref="ICentroidCluster{T}"/>.
        /// </summary>
        T Centroid { get; }
    }
}
