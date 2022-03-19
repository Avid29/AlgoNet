// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// A Shape for geometric points in a cluster.
    /// </summary>
    /// <remarks>
    /// Geometric points are defined by their absolute positions in space.
    /// </remarks>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    /// <typeparam name="TDistance">The type of floating point used for distance.</typeparam>
    public interface IGeometricPoint<T, TDistance> : IMetricPoint<T, TDistance>
        where TDistance : IFloatingPoint<TDistance>
    {
        /// <summary>
        /// Gets the average value of a list of <typeparamref name="T"/> items.
        /// </summary>
        /// <param name="items">The list of points to average.</param>
        /// <returns>The average of all items in the enumerable.</returns>
        T Average(T[] items);

        /// <summary>
        /// Gets the weighted average value of a list of (T, TWeight) by point and weight.
        /// </summary>
        /// <param name="items">A weighted list of points.</param>
        /// <returns>The weighted center of the points.</returns>
        T WeightedAverage<TWeight>((T, TWeight)[] items) where TWeight : INumber<TWeight>;
    }
}
