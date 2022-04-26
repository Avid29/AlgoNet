// Adam Dernis © 2021

namespace AlgoNet.Clustering
{
    /// <summary>
    /// A Shape for geometric points in a cluster.
    /// </summary>
    /// <remarks>
    /// Geometric points are defined by their absolute positions in space.
    /// </remarks>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    public interface IGeometricSpace<T> : IMetricSpace<T>, IAverageSpace<T>, IWeightedAverageSpace<T>, IRoundableSpace<T>
    {
    }
}
