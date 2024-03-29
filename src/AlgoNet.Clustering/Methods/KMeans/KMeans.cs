﻿// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A static class containing KMeans methods.
    /// </summary>
    public static class KMeans
    {
        /// <summary>
        /// Runs KMeans cluster on a list of <typeparamref name="T"/> points.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <param name="points">A list of points to cluster.</param>
        /// <param name="config">The configuration to run KMeans with.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>An array of weighted clusters based on their prevelence in the points.</returns>
        public static KMeansCluster<T, TShape>[] Cluster<T, TShape>(
            ReadOnlySpan<T> points,
            KMeansConfig<T, TShape> config,
            TShape shape)
            where T : unmanaged
            where TShape : struct, IDistanceSpace<T>, IAverageSpace<T>
        {
            // Split to arbitrary clusters
            KMeansCluster<T, TShape>[] clusters = Split<T, TShape>(points, config.ClusterCount);

            // Run no items change cluster on iteration.
            bool changed = true;
            while (changed)
            {
                changed = false;

                // For each point in each cluster
                for (int i = 0; i < clusters.Length; i++)
                {
                    KMeansCluster<T, TShape> cluster = clusters[i];
                    for (int pointIndex = 0; pointIndex < cluster.Points.Count; pointIndex++)
                    {
                        T point = cluster[pointIndex];

                        // Find the nearest cluster and move the item to it.
                        int nearestClusterIndex = FindNearestClusterIndex(point, clusters, shape);
                        if (nearestClusterIndex != i)
                        {
                            // A cluster can't be made empty. Leave the item in place if the cluster would be empty
                            if (cluster.Points.Count > 1)
                            {
                                T removedPoint = cluster.RemoveAt(pointIndex);
                                clusters[nearestClusterIndex].Add(removedPoint);
                                changed = true;
                            }
                        }
                    }
                }
            }

            return clusters;
        }

        /// <summary>
        /// Find the nearest cluster to a point.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <param name="point">The point to find a nearest cluster for.</param>
        /// <param name="clusters">The list of clusters.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>The index in <paramref name="clusters"/> of the nearest cluster to <paramref name="point"/>.</returns>
        private static int FindNearestClusterIndex<T, TShape>(
            T point,
            KMeansCluster<T, TShape>[] clusters,
            TShape shape)
            where T : unmanaged
            where TShape : struct, IDistanceSpace<T>, IAverageSpace<T>
        {
            // Track nearest seen value and its index.
            double minimumDistance = double.PositiveInfinity;
            int nearestClusterIndex = -1;

            for (int k = 0; k < clusters.Length; k++)
            {
                double distance = shape.FindDistanceSquared(point, clusters[k].Centroid);

                // Update tracking variables
                if (minimumDistance > distance)
                {
                    minimumDistance = distance;
                    nearestClusterIndex = k;
                }
            }

            // Return index of nearest cluster
            return nearestClusterIndex;
        }

        /// <summary>
        /// Splits a list of points in to arbitrary clusters.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The shape to use on the points to cluster.</typeparam>
        /// <param name="points">The list of points to place into clusters.</param>
        /// <param name="clusterCount">The amount of clusters to create.</param>
        /// <returns>A list of arbitrary clusters of size <paramref name="clusterCount"/> made out of the points in <paramref name="points"/>.</returns>
        private static KMeansCluster<T, TShape>[] Split<T, TShape>(
            ReadOnlySpan<T> points,
            int clusterCount)
            where T : unmanaged
            where TShape : struct, IDistanceSpace<T>, IAverageSpace<T>
        {
#if DEBUG
            // In-method input validation should only run in DEBUG. KMeansConfig runs input validation regardless, so this should never be hit.
            if (clusterCount <= 1)
            {
                throw new ArgumentOutOfRangeException(nameof(clusterCount), clusterCount, $"{nameof(clusterCount)} must be greater than or equal to two (2).");
            }
#endif

            KMeansCluster<T, TShape>[] clusters = new KMeansCluster<T, TShape>[clusterCount];
            int subSize = points.Length / clusterCount;

            int iterationPos = 0;
            for (int i = 0; i < clusterCount; i++)
            {
                KMeansCluster<T, TShape> currentCluster = new KMeansCluster<T, TShape>();

                // Until the cluster is full or the enumerator is out of elements.
                for (int j = 0; j < subSize && iterationPos < points.Length; j++)
                {
                    // Add element to current cluster and advance iteration.
                    currentCluster.Add(points[iterationPos]);
                    iterationPos++;
                }

                clusters[i] = currentCluster;
            }

            return clusters;
        }
    }
}
