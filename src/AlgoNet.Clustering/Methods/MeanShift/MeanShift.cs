// Adam Dernis © 2021

using AlgoNet.Clustering.Kernels;
using AlgoNet.Clustering.Methods.MeanShift;
using Microsoft.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AlgoNet.Clustering
{
    /// MeanShift finds clusters by moving clusters towards a convergence point.
    /// This initial position of a cluster is a clone of a coresponding point. If clusters share a position they can be merged into one.
    ///
    /// Mathematically, the convergence point can be found by graphing the distribution from each point.
    /// After summing these distributions, the nearest local maxima to a cluster's initial
    /// position is that cluster's convergence point.
    ///
    /// Clusters at 1, 4, 4.5 and 5. Overlayed
    ///
    ///           *                             *    *    *
    ///         *   *                         *   **   **   *
    ///       *       *                     *    *  * *  *    *
    ///     *           *                 *    *    * *    *    *
    ///   *               *             *    *    *     *    *    *
    /// *                   *         *    *    *         *    *    *
    /// 0 - - - - 1 - - - - 2 - - - - 3 - - - - 4 - - - - 5 - - - - 6
    ///           ·                             ·    ·    ·
    ///
    /// Clusters at 1, 4, 4.5 and 5. Summed
    ///
    ///                                              *
    ///                                            *   *
    ///                                          *       *
    ///                                         *         *
    ///                                         *         *
    ///                                        *           *
    ///                                       *             *
    ///           *                          *               *
    ///         *   *                       *                 *
    ///       *       *                    *                   *
    ///     *           *                 *                     *
    ///   *               *             *                         *
    /// *                   *         *                             *
    /// 0 - - - - 1 - - - - 2 - - - - 3 - - - - 4 - - - - 5 - - - - 6
    ///           ·                             ·    ·    ·
    ///
    /// The clusters would be 1 and 4.5, because those are all the local maximas.
    /// The clusters weighted would be (1, 1) and (4.5, 3) because 1 point went to the local max at 1 and 3 points went to the local max at 3.
    ///
    ///
    /// Programmatically, these clusters are found by continually shifting each cluster towards their convergence point.
    /// Each shift is performed by finding the cluster's distance from each point then weighting its effect on the cluster.
    /// These weights are then used to find a weighted average, the result of each is the new cluster position.

    public static class MeanShift
    {
        private const double ACCEPTED_ERROR = 0.000005;

        /// <summary>
        /// Clusters a set of points using MeanShift over a field.
        /// </summary>
        /// <remarks>
        /// It is usually wise to use WeightedMeanShift instead unless all points are unique.
        /// Weighted MeanShift greatly reduces computation time when dealing with duplicate points.
        /// </remarks>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TKernel">The type of kernel to use on the cluster.</typeparam>
        /// <param name="points">The points to shift until convergence.</param>
        /// <param name="field">The field of points to converge over.</param>
        /// <param name="kernel">The kernel to use for clustering.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>An array of clusters weighted by the contributing points.</returns>
        public static unsafe (T, int)[] ClusterRaw<T, TShape, TKernel>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            TKernel kernel,
            TShape shape)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Clone points into a modifiable list of clusters
            T[] clusters = new T[points.Length];

            // This array will be reused on every iteration
            // However we allocate it here once to save on allocation time and space
            (T, double)[] fieldWeights = new (T, double)[field.Length];

            fixed (T* f = field)
            {
                // Shift each cluster to its convergence point.
                for (int i = 0; i < clusters.Length; i++)
                {
                    T point = clusters[i];
                    T cluster = MeanShiftPoint(point, f, points.Length, shape, kernel, fieldWeights);
                    clusters[i] = cluster;

                    // TODO: Track points in the cluster
                }
            }

            return PostProcess(clusters, kernel, shape);
        }

        private static unsafe T MeanShiftPoint<T, TShape, TKernel>(
            T cluster,
            T* field,
            int fieldSize,
            TShape shape,
            TKernel kernel,
            (T, double)[] fieldWeights)
            where T : unmanaged
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            bool changed = true;
            T newCluster;

#if NET6_0_OR_GREATER
            Unsafe.SkipInit(out newCluster);
#endif

            // Shift point until it converges
            while (changed)
            {
                // Determine weight of all field points from the cluster's current position.
                for (int i = 0; i < fieldSize; i++)
                {
                    T point = field[i];
                    double distSqrd = shape.FindDistanceSquared(cluster, point);
                    double weight = kernel.WeightDistance(distSqrd);
                    fieldWeights[i] = (point, weight);
                }

                newCluster = shape.WeightedAverage(fieldWeights);
                changed = !shape.AreEqual(newCluster, cluster, ACCEPTED_ERROR);
                cluster = newCluster;
            }

            return cluster;
        }

        private static (T, int)[] PostProcess<T, TShape, TKernel>(
            T[] clusters,
            TKernel kernel,
            TShape shape)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
            where TKernel : struct, IKernel
        {
            // Remove explict duplicate values.
            DictionarySlim<T, int> mergeMap = new DictionarySlim<T, int>();
            foreach (var cluster in clusters)
            {
                mergeMap.GetOrAddValueRef(cluster)++;
            }

            // Connected componenents merge.
            // Because convergence may be imperfect, a minimum difference can be used to merge similar clusters.

            // Convert Dictionary to tuple list.
            (T, int)[] mergedCentroids = new (T, int)[mergeMap.Count];
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
