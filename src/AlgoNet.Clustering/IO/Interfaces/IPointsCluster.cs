// Adam Dernis © 2022

using System.Collections.Generic;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// An interface for Clusters that output with a collection points.
    /// </summary>
    /// <typeparam name="T">The type of data in the cluster.</typeparam>
    public interface IPointsCluster<T>
        where T : unmanaged
    {
        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> of points in the cluster.
        /// </summary>
        IReadOnlyCollection<T> Points { get; }
    }
}
