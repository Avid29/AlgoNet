﻿// Adam Dernis © 2022

namespace AlgoNet.Clustering
{
    /// <summary>
    /// An interface for a space where the average of a set of objects can be taken.
    /// </summary>
    public interface IAverageSpace<T> : ISpace<T>
    {
        /// <summary>
        /// Gets the average value of a list of <typeparamref name="T"/> items.
        /// </summary>
        /// <param name="items">The list of points to average.</param>
        /// <returns>The average of all items in the enumerable.</returns>
        T Average(T[] items);
    }
}
