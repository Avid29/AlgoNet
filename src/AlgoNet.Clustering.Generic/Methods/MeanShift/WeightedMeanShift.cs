// Adam Dernis © 2021

using AlgoNet.Clustering.Generic.Kernels;
using AlgoNet.Clustering.Generic.Shapes;
using Microsoft.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A static class containing Weighted Mean Shift methods.
    /// </summary>
    public static class WeightedMeanShift
    {
        private const double ACCEPTED_ERROR = 0.000005;

        /// <inheritdoc cref="Cluster{T, TShape, TKernel, TWeight, TDistance}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/>
        public static List<MSCluster<T, TShape, TWeight, TDistance>> Cluster<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<T> points,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance> => Cluster<T, TShape, TKernel, TWeight, TDistance>(points, points, kernel, shape);

        /// <summary>
        /// Clusters a set of points using a weighted version of MeanShift over a field by merging equal points.
        /// </summary>
        /// <remarks>
        /// If all points are unique, it is wise to use <see cref="MeanShift.Cluster{T, TShape, TKernel, TDistance}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/> instead since there's no duplicates to merge.
        /// </remarks>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
        /// <typeparam name="TWeight">The type of number used for weight.</typeparam>
        /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
        /// <param name="points">The points to shift until convergence.</param>
        /// <param name="field">The field of points to converge over.</param>
        /// <param name="kernel">The kernel to use for clustering.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>A list of clusters weighted by the contributing points.</returns>
        public static List<MSCluster<T, TShape, TWeight, TDistance>> Cluster<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight> 
            where TDistance : unmanaged, IFloatingPoint<TDistance> =>
            Wrap<T, TShape, TWeight, TDistance>(ClusterRaw<T, TShape, TKernel, TWeight, TDistance>(points, field, kernel, shape));

        /// <inheritdoc cref="Cluster{T, TShape, TKernel, TWeight, TDistance}(ReadOnlySpan{(T, TWeight)}, ReadOnlySpan{(T, TWeight)}, TKernel, TShape)"/>
        public static List<MSCluster<T, TShape, TWeight, TDistance>> Cluster<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<(T, TWeight)> points,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance> => Cluster<T, TShape, TKernel, TWeight, TDistance>(points, points, kernel, shape);

        /// <summary>
        /// Clusters a set of weighted points using MeanShift over a weighted field.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
        /// <typeparam name="TWeight">The type of number used for weight.</typeparam>
        /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
        /// <param name="points">The weighted points to shift until convergence.</param>
        /// <param name="field">The field of weighted points to converge over.</param>
        /// <param name="kernel">The kernel to use for clustering.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>A list of clusters weighted by the contributing points.</returns>
        public static List<MSCluster<T, TShape, TWeight, TDistance>> Cluster<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<(T, TWeight)> points,
            ReadOnlySpan<(T, TWeight)> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance> =>
            Wrap<T, TShape, TWeight, TDistance>(ClusterRaw<T, TShape, TKernel, TWeight, TDistance>(points, field, kernel, shape));

        /// <remarks>
        /// If all points are unique, it is wise to use <see cref="MeanShift.ClusterRaw{T, TShape, TKernel, TDistance}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/> instead since there's no duplicates to merge.
        /// </remarks>
        /// <returns>An array clusters weighted by their contributing points.</returns>
        /// <inheritdoc cref="Cluster{T, TShape, TKernel, TWeight, TDistance}(ReadOnlySpan{T}, ReadOnlySpan{T}, TKernel, TShape)"/>
        public static (T, TWeight)[] ClusterRaw<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance> =>
            ClusterRaw<T, TShape, TKernel, TWeight, TDistance>(MakeWeighted<T, TWeight>(points), MakeWeighted<T, TWeight>(field), kernel, shape);

        /// <returns>An array clusters weighted by their contributing points.</returns>
        /// <inheritdoc cref="Cluster{T, TShape, TKernel, TWeight, TDistance}(ReadOnlySpan{(T, TWeight)}, ReadOnlySpan{(T, TWeight)}, TKernel, TShape)"/>
        public static unsafe (T, TWeight)[] ClusterRaw<T, TShape, TKernel, TWeight, TDistance>(
            ReadOnlySpan<(T, TWeight)> points,
            ReadOnlySpan<(T, TWeight)> field,
            TKernel kernel,
            TShape shape = default)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance>
        {
            // Points will bed cloned into a modifiable list of clusters
            (T, TWeight)[] clusters = new(T, TWeight)[points.Length];

            // This array will be reused on every iteration
            // However we allocate it here once to save on allocation time and space
            (T, TWeight)[] fieldWeights = new(T, TWeight)[field.Length];

            fixed ((T, TWeight)* p = points)
            {
                for (int i = 0; i < clusters.Length; i++)
                {
                    (T, TWeight) point = points[i];
                    T clusterPoint = MeanShiftPoint<T, TShape, TKernel, TWeight, TDistance>(point.Item1, p, points.Length, shape, kernel, fieldWeights);
                    (T, TWeight) cluster = (clusterPoint, point.Item2);
                    clusters[i] = cluster;
                }
            }

            return PostProcess<T, TShape, TKernel, TWeight, TDistance>(clusters, kernel, shape);
        }

        /// <summary>
        /// Merges a set of points into a list of weighted unique points.
        /// </summary>
        private static ReadOnlySpan<(T, TWeight)> MakeWeighted<T, TWeight>(ReadOnlySpan<T> points)
            where T : unmanaged, IEquatable<T>
            where TWeight : INumber<TWeight>
        {
            // Merge equal points
            DictionarySlim<T, TWeight> merged = new();
            foreach (var point in points) merged.GetOrAddValueRef(point)++;

            // Convert back to tuple array
            (T, TWeight)[] weightedPoints = new(T, TWeight)[merged.Count];
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
        private static List<MSCluster<T, TShape, TWeight, TDistance>> Wrap<T, TShape, TWeight, TDistance>((T, TWeight)[] raw)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TWeight : INumber<TWeight>
            where TDistance : unmanaged, IFloatingPoint<TDistance>
        {
            List<MSCluster<T, TShape, TWeight, TDistance>> clusters = new();
            foreach (var cluster in raw)
            {
                clusters.Add(new MSCluster<T, TShape, TWeight, TDistance>(cluster.Item1, cluster.Item2));
            }

            return clusters;
        }

        /// <summary>
        /// Shifts a single point to its convergence point.
        /// </summary>
        private static unsafe T MeanShiftPoint<T, TShape, TKernel, TWeight, TDistance>(
            T cluster,
            (T, TWeight)* field,
            int fieldSize,
            TShape shape,
            TKernel kernel,
            (T, TWeight)[] fieldWeights)
            where T : unmanaged
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            bool changed = true;

            Unsafe.SkipInit(out T newCluster);

            // Shift point until it converges
            while (changed)
            {
                // Determine weight of all field points from the cluster's current position.
                for (int i = 0; i < fieldSize; i++)
                {
                    (T, TWeight) point = field[i];
                    TDistance distSqrd = shape.FindDistanceSquared(cluster, point.Item1);
                    TDistance weight = kernel.WeightDistance(distSqrd);
                    fieldWeights[i] = (point.Item1, TWeight.Create(weight) * point.Item2);
                }

                newCluster = shape.WeightedAverage(fieldWeights);
                changed = !shape.AreEqual(newCluster, cluster, TDistance.Create(ACCEPTED_ERROR));
                cluster = newCluster;
            }

            return cluster;
        }

        /// <summary>
        /// Merges converged points.
        /// </summary>
        private static (T, TWeight)[] PostProcess<T, TShape, TKernel, TWeight, TDistance>(
            (T, TWeight)[] clusters,
            TKernel kernel,
            TShape shape)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T, TDistance>
            where TKernel : struct, IKernel<TDistance>
            where TWeight : unmanaged, INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            // Remove explict duplicate values.
            DictionarySlim<T, TWeight> mergeMap = new();

            foreach (var cluster in clusters) mergeMap.GetOrAddValueRef(cluster.Item1) += cluster.Item2;

            // Connected componenents merge.
            // Because convergence may be imperfect, a minimum difference can be used to merge similar clusters.

            // Convert Dictionary to tuple list.
            (T, TWeight)[] mergedCentroids = new(T, TWeight)[mergeMap.Count];
            int i = 0;
            foreach (var value in mergeMap)
            {
                mergedCentroids[i] = (value.Key, value.Value);
                i++;
            }

            // Connected componenents merge using DBSCAN with a minPoints of 0.
            // Because convergence may be imperfect, a minimum difference can be used to merge similar clusters.
            // A wrapping shape must be used inorder to cluster the weighted points.
            DBSConfig<(T, TWeight), GenericWeightedShape<T, TShape, TWeight, TDistance>, TDistance> config = new(kernel.WindowSize, 0);
            GenericWeightedShape<T, TShape, TWeight, TDistance> weightedShape = new(shape);
            var results = DBSCAN.Cluster<(T, TWeight), GenericWeightedShape<T, TShape, TWeight, TDistance>, TWeight, TDistance>(mergedCentroids, config, weightedShape);

            // No components to merge
            if (mergedCentroids.Length == results.Count) return mergedCentroids;
            
            // Convert the DBSCAN clusters into centroids.
            mergedCentroids = new(T, TWeight)[results.Count];
            for (i = 0; i < results.Count; i++)
            {
                // Track the weight of each point the DBSCAN cluster
                TWeight weightSum = TWeight.Zero;

                // Pull the points from the DBSCAN cluster in order to take the average.
                (T, TWeight)[] points = new(T, TWeight)[results[i].Points.Count];
                for (int j = 0; j < results[i].Points.Count; j++)
                {
                    points[j] = results[i].Points[j];
                    weightSum += results[i].Points[j].Item2;
                }

                // Cache the weighted average of the points in the DBSCAN cluster
                // and the sum of their weights
                T point = shape.WeightedAverage(points);
                mergedCentroids[i] = (point, weightSum);
            }

            return mergedCentroids;
        }
    }
}
