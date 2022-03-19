// Adam Dernis © 2022

using System.Collections.Generic;

namespace AlgoNet.Graphs.TopologicalSorting
{
    /// <summary>
    /// A static class containing Kahn's algorithm methods.
    /// </summary>
    public static class Kahns
    {
        /// <summary>
        /// Sorts a graphic topologically using Kahn's algorithm.
        /// </summary>
        /// <typeparam name="T">The type of nodes in the graph.</typeparam>
        /// <typeparam name="TShape">The type of shape to use assessing nodes as a graph.</typeparam>
        /// <param name="graph">A list of nodes that make a graph</param>
        /// <param name="shape">A shape that can determine edges for the graph.</param>
        /// <returns>A topological ording of the graph.</returns>
        public static List<T> Sort<T, TShape>(T[] graph, TShape shape = default)
            where TShape : struct, INode<T>
        {
            // Counts the inward edges on each node.
            Dictionary<T, int> edgesIn = new Dictionary<T, int>(graph.Length);
            foreach (T node in graph)
            {
                IEnumerable<T> connections = shape.GetConnectedNodes(node);
                foreach (T connection in connections)
                {
                    edgesIn[connection]++;
                }
            }

            // Queue root nodes (nodes with no inward edges)
            Queue<T> queue = new Queue<T>();
            foreach (KeyValuePair<T, int> pair in edgesIn)
            {
                if (pair.Value == 0)
                    queue.Enqueue(pair.Key);
            }

            // Tracks the number of verticies vistited
            int i = 0;

            List<T> order = new List<T>(graph.Length);
            while (queue.Count > 0)
            {
                T u = queue.Dequeue();
                order.Add(u);

                var connected = shape.GetConnectedNodes(u);
                foreach (var v in connected)
                {
                    if (--edgesIn[v] == 0)
                        queue.Enqueue(v);
                }

                i++;
            }

            // Return null if a cycle occured
            if (i != graph.Length)
            {
                return null;
            }

            return order;
        }
    }
}
