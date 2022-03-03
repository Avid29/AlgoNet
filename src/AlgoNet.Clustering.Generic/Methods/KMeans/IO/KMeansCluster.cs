// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A <see cref="Cluster{T, TShape}"/> implementation for KMeans.
    /// </summary>
    /// <typeparam name="T">The type of data in the cluster.</typeparam>
    /// <typeparam name="TShape">A shape to describe to provide comparison methods for <typeparamref name="T"/>.</typeparam>
    public class KMeansCluster<T, TShape, TWeight, TDistance> : Cluster<T, TShape, TDistance>, ICentroidCluster<T>, IPointsCluster<T>, IWeightedCluster<TWeight>
        where T : unmanaged
        where TShape : struct, IGeometricPoint<T, TWeight, TDistance>
        where TWeight : INumber<TWeight>
        where TDistance : IFloatingPoint<TDistance>
    {
        private T? _centroid;

        /// <summary>
        /// Initializes a new instance of the <see cref="KMeansCluster{T, TShape}"/> class.
        /// </summary>
        public KMeansCluster()
        {
        }

        /// <inheritdoc/>
        public T Centroid => _centroid ?? (T)(_centroid = CalculateCentroid());

        /// <inheritdoc/>
        IReadOnlyCollection<T> IPointsCluster<T>.Points => Points;

        /// <summary>
        /// Gets a list of points in the cluster.
        /// </summary>
        internal List<T> Points { get; } = new List<T>();

        /// <inheritdoc/>
        public TWeight Weight => TWeight.Create(Points.Count);

        /// <summary>
        /// Gets or sets an element at an index in the sub point list.
        /// </summary>
        /// <param name="i">The index of the element to get or set.</param>
        /// <returns>The element at index <paramref name="i"/> in the sub point list.</returns>
        public T this[int i]
        {
            get => Points[i];
            set => Points[i] = value;
        }

        /// <summary>
        /// Adds a point to the <see cref="KMeansCluster{T, TShape}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            Points.Add(item);
            _centroid = null;
        }

        /// <summary>
        /// Removes the point at <paramref name="index"/> from the sub point list.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns>The removed item.</returns>
        public T RemoveAt(int index)
        {
            T removed = Points[index];
            Points.RemoveAt(index);
            _centroid = null;
            return removed;
        }

        /// <summary>
        /// Gets the element in the cluster closest to the Centroid.
        /// </summary>
        /// <returns>The element in the cluster nearest the Centroid.</returns>
        public T GetNearestToCenter()
        {
            TShape shape = default;

            // Track nearest seen value and its index.
            TDistance minimumDistance = TDistance.PositiveInfinity;
            int nearestPointIndex = -1;

            for (int i = 0; i < Points.Count; i++)
            {
                T p = Points[i];
                TDistance distance = shape.FindDistanceSquared(p, Centroid);

                // Update tracking variables
                if (minimumDistance > distance)
                {
                    minimumDistance = distance;
                    nearestPointIndex = i;
                }
            }

            // return the value at the index with the largest value found
            return Points[nearestPointIndex];
        }

        /// <summary>
        /// Calculates the Centroid for the values in the cluster.
        /// </summary>
        /// <remarks>
        /// Calculates the centroid entirely from scratch ignoring <see cref="_centroid"/>.
        /// </remarks>
        /// <returns>The center point of all values in the cluster.</returns>
        private T CalculateCentroid()
        {
            TShape shape = default;
            return shape.Average(Points.ToArray());
        }
    }
}
