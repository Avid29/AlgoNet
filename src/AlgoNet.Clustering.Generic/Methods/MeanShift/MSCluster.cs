// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A <see cref="Cluster{T, TShape}"/> implementation for the MeanShift algorithm.
    /// </summary>
    /// <typeparam name="T">The type of points in the cluster.</typeparam>
    /// <typeparam name="TShape">A shape to describe to provide comparison methods for <typeparamref name="T"/>.</typeparam>
    public class MSCluster<T, TShape, TWeight, TDistance> : Cluster<T, TShape, TDistance>, ICentroidCluster<T>, IWeightedCluster<TDistance>
        where T : unmanaged
        where TShape : struct, IGeometricPoint<T, TWeight, TDistance>
        where TWeight : INumber<TWeight>
        where TDistance : unmanaged, IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSCluster{T, TShape}"/> class.
        /// </summary>
        /// <param name="centroid">The centroid of the <see cref="MSCluster{T, TShape}"/>.</param>
        /// <param name="weight">The total weight of, or summed point count for the cluster.</param>
        internal MSCluster(T centroid, TWeight weight)
        {
            Centroid = centroid;
            Weight = weight;
        }

        /// <inheritdoc/>
        public T Centroid { get; }

        /// <inheritdoc/>
        public TDistance Weight { get; }
    }
}
