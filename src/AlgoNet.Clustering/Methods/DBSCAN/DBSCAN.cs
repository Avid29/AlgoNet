// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A static class containing DBSCAN methods.
    /// </summary>
    public static class DBSCAN
    {
        public static unsafe List<DBSCluster<T, TShape>> Cluster<T, TShape>(
            ReadOnlySpan<T> points,
            DBSConfig<T, TShape> config,
            TShape shape = default)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T>
        {
            List<DBSCluster<T, TShape>> clusters = new List<DBSCluster<T, TShape>>();

            fixed (T* p = points)
            {
                // Create a DBSContext to avoid passing too many values between functions
                DBSContext<T, TShape> context = new DBSContext<T, TShape>(config, shape, p, points.Length);

                for (int i = 0; i < points.Length; i++)
                {
                    T point = p[i];
                    if (context.ClusterIds[i] == DBSConstants.UNCLASSIFIED_ID)
                    {
                        DBSCluster<T, TShape> cluster = CreateCluster(point, i, context);
                        if (cluster != null) clusters.Add(cluster);
                    }
                }

                // Add noise (if applicable)
                if (context.ReturnNoise) clusters.Add(context.NoiseCluster);
            }

            return clusters;
        }

        private static DBSCluster<T, TShape> CreateCluster<T, TShape>(
            T p,
            int i,
            DBSContext<T, TShape> context)
            where T : unmanaged
            where TShape: struct, IMetricPoint<T>
        {

            // Create cluster with the next cluster Id.
            DBSCluster<T, TShape> cluster = new DBSCluster<T, TShape>(context.NextClusterId);
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
                for (int j = 0; j < seeds.Count; j++) context.ClusterIds[seeds[j].Item2] = cluster.ClusterId;
                seeds.Remove((p, i));
                ExpandCluster(cluster, seeds, context);
                return cluster;
            }
        }

        private static void ExpandCluster<T, TShape>(
            DBSCluster<T, TShape> cluster,
            List<(T, int)> seeds,
            DBSContext<T, TShape> context)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T>
        {
            // Seeds is used as a queue for breadth-first graph traversal
            while (seeds.Count > 0)
            {
                // Find 
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
            }
        }

        private unsafe static List<(T, int)> GetSeeds<T, TShape>(
            T p,
            DBSContext<T, TShape> context)
            where T : unmanaged
            where TShape : struct, IMetricPoint<T>
        {
            List<(T, int)> seeds = new List<(T, int)>();
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
   