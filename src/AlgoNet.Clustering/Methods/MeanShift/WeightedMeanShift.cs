// Adam Dernis © 2021

using AlgoNet.Clustering.Kernels;
using Microsoft.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// 
    /// </summary>
    public static class WeightedMeanShift
    {
        private const double ACCEPTED_ERROR = 0.000005;

        /// <inheritdoc cref="Cluster{T, TShape, TKernel}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/>
        public static List<MSCluster<T, TShape>> Cluster<T, TShape, TKernel>(
            ReadOnlySpan<T> points,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            return Cluster(points, points, kernel, shape);
        }

        /// <summary>
        /// Clusters a set of points using a weighted version of MeanShift over a field by merging equal points.
        /// </summary>
        /// <remarks>
        /// If all points are unique, it is wise to use <see cref="MeanShift.Cluster{T, TShape, TKernel}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/> instead since there's no duplicates to merge.
        /// </remarks>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
        /// <param name="points">The points to shift until convergence.</param>
        /// <param name="field">The field of points to converge over.</param>
        /// <param name="kernel">The kernel to use for clustering.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>A list of clusters weighted by the contributing points.</returns>
        public static List<MSCluster<T, TShape>> Cluster<T, TShape, TKernel>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Take the regular raw cluster.
            (T, double)[] raw = ClusterRaw(points, field, kernel, shape);

            // Convert tuples into MSClusters
            return Wrap<T, TShape>(raw);
        }

        /// <inheritdoc cref="Cluster{T, TShape, TKernel}(ReadOnlySpan{(T, double)}, ReadOnlySpan{(T, double)}, TKernel, TShape)"/>
        public static List<MSCluster<T, TShape>> Cluster<T, TShape, TKernel>(
            ReadOnlySpan<(T, double)> points,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            return Cluster(points, points, kernel, shape);
        }

        /// <summary>
        /// Clusters a set of weighted points using MeanShift over a weighted field.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
        /// <param name="points">The weighted points to shift until convergence.</param>
        /// <param name="field">The field of weighted points to converge over.</param>
        /// <param name="kernel">The kernel to use for clustering.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>A list of clusters weighted by the contributing points.</returns>
        public static List<MSCluster<T, TShape>> Cluster<T, TShape, TKernel>(
            ReadOnlySpan<(T, double)> points,
            ReadOnlySpan<(T, double)> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Take the regular raw cluster.
            (T, double)[] raw = ClusterRaw(points, field, kernel, shape);

            // Convert tuples into MSClusters
            return Wrap<T, TShape>(raw);
        }

        /// <remarks>
        /// If all points are unique, it is wise to use <see cref="MeanShift.ClusterRaw{T, TShape, TKernel}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape){T, TShape, TKernel}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/> instead since there's no duplicates to merge.
        /// </remarks>
        /// <returns>An array clusters weighted by their contributing points.</returns>
        /// <inheritdoc cref="Cluster{T, TShape, TKernel}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/>
        public static (T, double)[] ClusterRaw<T, TShape, TKernel>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            ReadOnlySpan<(T, double)> weightedPoints = MakeWeighted(points);
            ReadOnlySpan<(T, double)> weightedField = MakeWeighted(field);
            return ClusterRaw(weightedPoints, weightedField, kernel, shape);
        }

        /// <returns>An array clusters weighted by their contributing points.</returns>
        /// <inheritdoc cref="Cluster{T, TShape, TKernel}(ReadOnlySpan{(T, double)}, ReadOnlySpan{(T, double)}, TKernel, TShape)"/>
        public static unsafe (T, double)[] ClusterRaw<T, TShape, TKernel>(
            ReadOnlySpan<(T, double)> points,
            ReadOnlySpan<(T, double)> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Clone points into a modifiable list of clusters
            (T, double)[] clusters = new (T, double)[points.Length];
            // This array will be reused on every iteration
            // However we allocate it here once to save on allocation time and space
            (T, double)[] fieldWeights = new (T, double)[field.Length];

            fixed ((T, double)* p = points)
            {
                for (int i = 0; i < clusters.Length; i++)
                {
                    (T, double) point = points[i];
                    T clusterPoint = MeanShiftPoint(point.Item1, p, points.Length, shape, kernel, fieldWeights);
                    (T, double) cluster = (clusterPoint, point.Item2);
                    clusters[i] = cluster;
                }
            }

            return PostProcess(clusters, kernel, shape);
        }

        /// <summary>
        /// Merges a set of points into a list of weighted unique points.
        /// </summary>
        private static ReadOnlySpan<(T, double)> MakeWeighted<T>(ReadOnlySpan<T> points)
            where T : unmanaged, IEquatable<T>
        {
            // Merge equal points
            DictionarySlim<T, int> merged = new();
            foreach (var point in points) merged.GetOrAddValueRef(point)++;

            // Convert back to tuple array
            (T, double)[] weightedPoints = new (T, double)[merged.Count];
            int i = 0;
            foreach (var entry in merged)
            {
                weightedPoints[i] = (entry.Key, entry.Value);
                i++;
            }

            return weightedPoints;
        }

        /// <summary>
        /// Takes an array of points and tuples and converts them to <see cref="MSCluster{T, TShape}"/>s.
        /// </summary>
        private static List<MSCluster<T, TShape>> Wrap<T, TShape>((T, double)[] raw)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
        {
            List<MSCluster<T, TShape>> clusters = new();
            foreach (var cluster in raw)
            {
                clusters.Add(new MSCluster<T, TShape>(cluster.Item1, cluster.Item2));
            }

            return clusters;
        }

        /// <summary>
        /// Shifts a single point to its convergence point.
        /// </summary>
        private static unsafe T MeanShiftPoint<T, TShape, TKernel>(
            T cluster,
            (T, double)* field,
            int fieldSize,
            TShape shape,
            TKernel kernel,
            (T, double)[] fieldWeights)
            where T : unmanaged
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            bool changed = true;

#if NET6_0_OR_GREATER
            Unsafe.SkipInit(out T newCluster);
#else
            T newCluster;
#endif

            // Shift point until it converges
            while (changed)
            {
                // Determine weight of all field points from the cluster's current position.
                for (int i = 0; i < fieldSize; i++)
                {
                    (T, double) point = field[i];
                    double distSqrd = shape.FindDistanceSquared(cluster, point.Item1);
                    double weight = kernel.WeightDistance(distSqrd);
                    fieldWeights[i] = (point.Item1, weight * point.Item2);
                }

                newCluster = shape.WeightedAverage(fieldWeights);
                changed = !shape.AreEqual(newCluster, cluster, ACCEPTED_ERROR);
                cluster = newCluster;
            }

            return cluster;
        }

        /// <summary>
        /// Merges converged points.
        /// </summary>
        private static (T, double)[] PostProcess<T, TShape, TKernel>(
            (T, double)[] clusters,
            TKernel kernel,
            TShape shape)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Remove explict duplicate values.
            DictionarySlim<T, double> mergeMap = new ();

            foreach (var cluster in clusters) mergeMap.GetOrAddValueRef(cluster.Item1) += cluster.Item2;

            // Connected componenents merge.
            // Because convergence may be imperfect, a minimum difference can be used to merge similar clusters.

            // Convert Dictionary to tuple list.
            (T, double)[] mergedCentroids = new (T, double)[mergeMap.Count];
            int i = 0;
            foreach (var value in mergeMap)
            {
                mergedCentroids[i] = (value.Key, value.Value);
                i++;
            }

            // TODO: Connected components cluter
            // Investigate: Can I use DBSCAN with minPoints of 1?

            return mergedCentroids;
        }
    }
}
