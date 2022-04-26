// Adam Dernis © 2022

namespace AlgoNet.Clustering
{
    /// <summary>
    /// An interface for a space where the weighted average of a set of objects can be taken.
    /// </summary>
    public interface IWeightedAverageSpace<T> : ISpace<T>
    {
        /// <summary>
        /// Gets the weighted average value of a list of (T, double) by point and weight.
        /// </summary>
        /// <param name="items">A weighted list of points.</param>
        /// <returns>The weighted center of the points.</returns>
        T WeightedAverage((T, double)[] items);
    }
}
