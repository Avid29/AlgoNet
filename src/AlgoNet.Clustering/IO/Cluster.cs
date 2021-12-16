// Adam Dernis © 2021

using AlgoNet.Clustering.Points;

namespace AlgoNet.Clustering.IO
{
    /// <summary>
    /// The base class for clusters.
    /// </summary>
    public abstract class Cluster<T, TShape>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T>
    {
    }
}
