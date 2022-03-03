// Adam Dernis © 2022

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// An interface for Clusters that output with a Weight.
    /// </summary>
    public interface IWeightedCluster<T>
        where T : INumber<T>
    {
        /// <summary>
        /// Gets the weight of the cluster.
        /// </summary>
        T Weight { get; }
    }
}
