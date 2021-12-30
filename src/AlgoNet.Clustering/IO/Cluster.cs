// Adam Dernis © 2021

namespace AlgoNet.Clustering
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
