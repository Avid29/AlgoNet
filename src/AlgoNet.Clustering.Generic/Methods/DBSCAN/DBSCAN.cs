﻿// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A static class containing DBSCAN methods.
    /// </summary>
    public static class DBSCAN
    {
        /// <summary>
        /// Clusters a set of points using DBScan.
        /// </summary>
        /// <typeparam name="T">The type of points to cluster.</typeparam>
        /// <typeparam name="TShape">The type of shape to use on the points to cluster.</typeparam>
        /// <typeparam name="TWeight">The type of number used for weight.</typeparam>
        /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
        /// <param name="points">The set of points to cluster.</param>
        /// <param name="config">A configuration for DBSCAN including epsilon and minPoints.</param>
        /// <param name="shape">The shape to use on the points to cluster.</param>
        /// <returns>A list of clusters.</returns>
        public static unsafe List<DBSCluster<T, TShape, TWeight, TDistance>> Cluster<T, TShape, TWeight, TDistance>(
            ReadOnlySpan<T> points,
            DBSConfig<T, TShape, TDistance> config,
            TShape shape = default)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T, TDistance>
            where TWeight : INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            List<DBSCluster<T, TShape, TWeight, TDistance>> clusters = new();

            fixed (T* p = points)
            {
                // Create a DBSContext to avoid passing too many values between functions
                DBSContext<T, TShape, TWeight, TDistance> context = new(config, shape, p, points.Length);

                for (int i = 0; i < points.Length; i++)
                {
                    // Attempt to create cluster if the point is unclassified
                    if (context.ClusterIds[i] == DBSConstants.UNCLASSIFIED_ID)
                    {
                        T point = p[i];
                        DBSCluster<T, TShape, TWeight, TDistance> cluster = CreateCluster(point, i, context);
                        if (cluster != null) clusters.Add(cluster);
                    }
                }

                // Add noise (if applicable)
                if (context.ReturnNoise) clusters.Add(context.NoiseCluster);
            }

            return clusters;
        }

        private static DBSCluster<T, TShape, TWeight, TDistance> CreateCluster<T, TShape, TWeight, TDistance>(
            T p,
            int i,
            DBSContext<T, TShape, TWeight, TDistance> context)
            where T : unmanaged
            where TShape: struct, IMetricPoint<T, TDistance>
            where TWeight : INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            // Create cluster with the next cluster Id.
            DBSCluster<T, TShape, TWeight, TDistance> cluster = new(context.NextClusterId);
            List<(T, int)> seeds = GetSeeds(p, context);
            if (seeds.Count < context.MinPoints)
            {
                // Not a core point
                // Assign noise id and return null
                context.ClusterIds[i] = DBSConstants.NOISE_ID;
                if (context.ReturnNoise) context.NoiseCluster.Points.Add(p);

                // Current next cluster id is not incremented because this was not a cluster
                return null;
            }
            else
            {
                // This is a valid cluster. Increment the cluster id.
                context.NextClusterId++;

                // Expand the cluster to include all seeds, and their seeds recursively
                // Use seeds as a queue of seeds to add to the cluster
                for (int j = 0; j < seeds.Count; j++)
                {
                    cluster.Points.Add(seeds[j].Item1);
                    context.ClusterIds[seeds[j].Item2] = cluster.ClusterId;
                }

                seeds.Remove((p, i));
                ExpandCluster(cluster, seeds, context);
                return cluster;
            }
        }

        private static void ExpandCluster<T, TShape, TWeight, TDistance>(
            DBSCluster<T, TShape, TWeight, TDistance> cluster,
            List<(T, int)> seeds,
            DBSContext<T, TShape, TWeight, TDistance> context)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T, TDistance>
            where TWeight  : INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            // Seeds is used as a queue for breadth-first graph traversal
            while (seeds.Count > 0)
            {
                // Find each point connected to this seed
                (T, int) p = seeds[0];
                List<(T, int)> pSeeds = GetSeeds(p.Item1, context);
                if (pSeeds.Count >= context.MinPoints)
                {
                    for (int i = 0; i < pSeeds.Count; i++)
                    {
                        (T, int) iP = seeds[0];
                        ref int iPId = ref context.ClusterIds[iP.Item2];

                        // If unclassified or noise, add to cluster
                        if (iPId == DBSConstants.UNCLASSIFIED_ID ||
                            iPId == DBSConstants.NOISE_ID)
                        {
                            // If unclassified, add to search queue
                            if (iPId == DBSConstants.UNCLASSIFIED_ID) seeds.Add(iP);

                            cluster.Points.Add(iP.Item1);
                            iPId = cluster.ClusterId;
                        }
                    }
                }

                seeds.RemoveAt(0);
            }
        }

        private unsafe static List<(T, int)> GetSeeds<T, TShape, TWeight, TDistance>(
            T p,
            DBSContext<T, TShape, TWeight, TDistance> context)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T, TDistance>
            where TWeight : INumber<TWeight>
            where TDistance : IFloatingPoint<TDistance>
        {
            List<(T, int)> seeds = new();
            for (int i = 0; i < context.PointsLength; i++)
            {
                if (context.Shape.FindDistanceSquared(p, context.Points[i]) <= context.Episilon2)
                {
                    seeds.Add((context.Points[i], i));
                }
            }

            return seeds;
        }
    }
}
   