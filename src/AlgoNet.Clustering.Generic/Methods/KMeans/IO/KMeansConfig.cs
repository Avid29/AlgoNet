// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A struct containing configuration info for KMeans Cluster to run.
    /// </summary>
    /// <typeparam name="T">The type of data in the cluster.</typeparam>
    /// <typeparam name="TShape">A shape to describe to provide comparison methods for <typeparamref name="T"/>.</typeparam>
    public class KMeansConfig<T, TShape, TWeight, TDistance>
        where T : unmanaged
        where TShape : struct, IGeometricPoint<T, TDistance>
        where TWeight : INumber<TWeight>
        where TDistance : IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KMeansConfig{T, TShape}"/> struct.
        /// </summary>
        /// <param name="clustCount">The number of clusters to create with KMeans. Must be greater than or equal to two (2).</param>
        public KMeansConfig(int clustCount)
        {
            if (clustCount < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(clustCount), clustCount, $"{nameof(clustCount)} must be greater than or equal to two (2).");
            }

            ClusterCount = clustCount;
        }

        /// <summary>
        /// Gets the number of clusters to create with KMeans.
        /// </summary>
        public int ClusterCount { get; }
    }
}
