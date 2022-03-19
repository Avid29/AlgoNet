// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A struct used to wrap the stack for DBSCAN.
    /// </summary>
    /// <typeparam name="T">The type of points to cluster.</typeparam>
    /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
    /// <typeparam name="TWeight">The type of number used for weight.</typeparam>
    /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
    internal unsafe ref struct DBSContext<T, TShape, TWeight, TDistance>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T, TDistance>
        where TWeight : INumber<TWeight>
        where TDistance : IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DBSContext{T, TShape}"/> struct.
        /// </summary>
        /// <param name="config">The configuration DBSCAN is running with.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <param name="points">The points being clustered.</param>
        /// <param name="pointsLength">The points number of points in <paramref name="points"/>.</param>
        public DBSContext(DBSConfig<T, TShape, TDistance> config, TShape shape, T* points, int pointsLength)
        {
            NextClusterId = 1;
            ClusterIds = new int[pointsLength];

            Episilon2 = config.Epsilon * config.Epsilon;
            MinPoints = config.MinPoints;

            NoiseCluster = config.ReturnNoise ? new DBSCluster<T, TShape, TWeight, TDistance>(DBSConstants.NOISE_ID) : null;

            Points = points;
            PointsLength = pointsLength;

            Shape = shape;
        }

        /// <summary>
        /// Gets or sets the next clusterId.
        /// </summary>
        public int NextClusterId { get; set; }

        /// <summary>
        /// Gets an array containing the cluster ids of each point in <see cref="Points"/>.
        /// </summary>
        public int[] ClusterIds { get; }

        /// <summary>
        /// Gets epsilon squared. The max distance squared to consider two points connected.
        /// </summary>
        public TDistance Episilon2 { get; }

        /// <summary>
        /// Gets the minimum number of connected points required to make a cluster.
        /// </summary>
        public int MinPoints { get; }

        /// <summary>
        /// Gets a cluster containing all noise points.
        /// </summary>
        /// <remarks>
        /// Null if not tracking noise.
        /// </remarks>
        public DBSCluster<T, TShape, TWeight, TDistance> NoiseCluster { get; }

        /// <summary>
        /// Gets a value indicating wether or not to return the noise cluster with the results.
        /// </summary>
        public bool ReturnNoise => NoiseCluster != null;

        /// <summary>
        /// Gets a pointer to a span containing all the points being clustered.
        /// </summary>
        public T* Points { get; }

        /// <summary>
        /// Gets the number of items in the <see cref="Points"/>.
        /// </summary>
        public int PointsLength { get; }

        /// <summary>
        /// Gets or sets the shape to use on the points to cluster. 
        /// </summary>
        public TShape Shape { get; }
    }
}
