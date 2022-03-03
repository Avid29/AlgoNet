// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A Shape for non-geometric points in a cluster.
    /// </summary>
    /// <remarks>
    /// Metric points are defined by having relative distances from each other, but no absolute position in an space.
    /// </remarks>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
    public interface IMetricPoint<T, TDistance>
        where TDistance : IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Gets the distance between <paramref name="it1"/> and <paramref name="it2"/>.
        /// </summary>
        /// <param name="it1">Point A.</param>
        /// <param name="it2">Point B.</param>
        /// <returns>The distance between point A and point B.</returns>
        TDistance FindDistanceSquared(T it1, T it2);

        /// <inheritdoc cref="AreEqual(T, T, TDistance)"/>
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
        bool AreEqual(T it1, T it2, TDistance error);
    }
}
