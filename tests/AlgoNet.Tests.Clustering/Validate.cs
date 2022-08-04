// Adam Dernis © 2022

using AlgoNet.Clustering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AlgoNet.Tests.Clustering
{
    public static class Validate
    {
        private static void ValidateCentroidCluster<T, TCluster, TShape>(TCluster cluster, List<TCluster> clusters, TShape shape)
            where T : unmanaged
            where TCluster : ICentroidCluster<T>, IPointsCluster<T>
            where TShape : IDistanceSpace<T>
        {
            foreach (var point in cluster.Points)
            {
                double dist2Canon = shape.FindDistanceSquared(cluster.Centroid, point);
                foreach (var c in clusters)
                {
                    if (c.Equals(cluster)) continue;

                    double dist2Available = shape.FindDistanceSquared(c.Centroid, point);
                    Assert.IsTrue(dist2Canon <= dist2Available);
                }
            }
        }

        public static void CentroidValidate<T, TCluster, TShape>(List<TCluster> clusters, TShape shape)
            where T : unmanaged
            where TCluster : ICentroidCluster<T>, IPointsCluster<T>
            where TShape : IDistanceSpace<T>
        {
            foreach(var cluster in clusters)
            {
                ValidateCentroidCluster<T, TCluster, TShape>(cluster, clusters, shape);
            }
        }
    }
}
