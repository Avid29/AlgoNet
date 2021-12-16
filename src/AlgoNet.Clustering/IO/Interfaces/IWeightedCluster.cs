// Adam Dernis © 2021

namespace AlgoNet.Clustering.IO.Interfaces
{
    /// <summary>
    /// An interface for Clusters that output with a Weight.
    /// </summary>
    public interface IWeightedCluster
    {
        /// <summary>
        /// Gets the weight of the cluster.
        /// </summary>
        double Weight { get; }
    }
}
