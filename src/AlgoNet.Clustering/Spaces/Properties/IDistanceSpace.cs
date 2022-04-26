// Adam Dernis © 2022

namespace AlgoNet.Clustering
{
    /// <summary>
    /// An interface for a space where the distance between objects is determinable.
    /// </summary>
    public interface IDistanceSpace<T> : ISpace<T>
    {
        /// <summary>
        /// Gets the distance between <paramref name="it1"/> and <paramref name="it2"/>.
        /// </summary>
        /// <param name="it1">Point A.</param>
        /// <param name="it2">Point B.</param>
        /// <returns>The distance between point A and point B.</returns>
        double FindDistanceSquared(T it1, T it2);
    }
}
