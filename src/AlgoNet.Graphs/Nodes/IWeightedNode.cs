// Adam Dernis © 2022

namespace AlgoNet.Graphs
{
    /// <summary>
    /// A Shape for Nodes in a graph where edges have a length.
    /// </summary>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    public interface IWeightedNode<T> : INode<T>
    {
        /// <summary>
        /// Gets the distance from one node to another.
        /// </summary>
        /// <param name="node1">The source node.</param>
        /// <param name="node2">The target node.</param>
        /// <returns>The distance of the edge or <see langword="double.NaN"/> if there is no edge</returns>
        double GetEdgeLength(T node1, T node2);
    }
}
