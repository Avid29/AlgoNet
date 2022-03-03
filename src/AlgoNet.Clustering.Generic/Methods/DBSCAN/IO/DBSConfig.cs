// Adam Dernis © 2022

using Microsoft.Toolkit.Diagnostics;
using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A struct containing configuration info for DBSCAN Cluster to run.
    /// </summary>
    /// <typeparam name="T">The type of data in the cluster.</typeparam>
    /// <typeparam name="TShape">The type of shape to use on the points while clustering.</typeparam>
    /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
    public struct DBSConfig<T, TShape, TDistance>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T, TDistance>
        where TDistance : IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBSConfig{T, TShape}"/> struct.
        /// </summary>
        /// <param name="eps">The maximum distance designated as a connected point.</param>
        /// <param name="minPts">The minimum number of points to form a region.</param>
        /// <param name="returnNoise">Indicates if a Cluster containing all noise points should be appended to the resultant cluster list.</param>
        public DBSConfig(TDistance eps, int minPts, bool returnNoise = false)
        {
            Guard.IsGreaterThan(eps, TDistance.Zero, nameof(eps));
            Guard.IsGreaterThanOrEqualTo(minPts, 0, nameof(minPts));

            ReturnNoise = returnNoise;
            Epsilon = eps;
            MinPoints = minPts;
        }

        /// <summary>
        /// Gets a value indicating whether or not to append the Cluster list with a cluster containing all noise points.
        /// </summary>
        public bool ReturnNoise { get; }

        /// <summary>
        /// Gets the maximum distance to consider two connected points.
        /// </summary>
        public TDistance Epsilon { get; }

        /// <summary>
        /// Gets the minimum number of points to consider a cluster.
        /// </summary>
        public int MinPoints { get; }
    }
}
