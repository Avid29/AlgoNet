// Adam Dernis © 2022

using System.Collections.Generic;

namespace AlgoNet.Graphs
{
    /// <summary>
    /// A Shape for Nodes in a graph.
    /// </summary>
    /// <typeparam name="T">The type being wrapped by the implementation.</typeparam>
    public interface INode<T>
    {
        /// <summary>
        /// Checks if two nodes the distance between two nodes.
        /// </summary>
        /// <param name="node1">The source node.</param>
        /// <param name="node2">The target node.</param>
        /// <returns>True if the nodes share an edge.</returns>
        bool CheckEdge(T node1, T node2);

        /// <summary>
        /// Gets a list of connected nodes to <paramref name="node"/>.
        /// </summary>
        /// <param name="node">The node to get the connected nodes to.</param>
        /// <returns>Connected nodes</returns>
        IEnumerable<T> GetConnectedNodes(T node);
    }
}
