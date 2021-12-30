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
    public interface IMetricPoint<T>
    {
        /// <summary>
        /// Gets the distance between <paramref name="it1"/> and <paramref name="it2"/>.
        /// </summary>
        /// <param name="it1">Point A.</param>
        /// <param name="it2">Point B.</param>
        /// <returns>The distance between point A and point B.</returns>
        double FindDistanceSquared(T it1, T it2);

        /// <inheritdoc cref="AreEqual(T, T, double)"/>
        bool AreEqual(T it1, T it2);

        /// <summary>
        /// Checks equality of two items.
        /// </summary>
        /// <remarks>
        /// For if there is an optimized way of comparing two points for equality.
        /// </remarks>
        /// <param name="it1">The item to compare.</param>
        /// <param name="it2">The item to compare it to.</param>
        /// <param name="error">The accepted error by distance for a comparison.</param>
        /// <returns>Whether or not the items are equal.</returns>
        bool AreEqual(T it1, T it2, double error);
    }
}
