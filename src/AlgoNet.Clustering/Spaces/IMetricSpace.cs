// Adam Dernis © 2021

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A Shape for non-geometric points in a cluster.
    /// </summary>
    /// <remarks>
    /// Metric points are defined by having relative distances from each other, but no absolute position in an space.
    /// </remarks>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    public interface IMetricSpace<T> : IDistanceSpace<T>, IEquatableSpace<T>
    {
    }
}
