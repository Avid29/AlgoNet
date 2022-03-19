// Adam Dernis © 2022

using AlgoNet.Graphs;
using System.Collections.Generic;

namespace AlgoNet.Tests.Graph.Shapes
{
    /// <summary>
    /// A shape for treating integers as a linear graph.
    /// </summary>
    public struct LinearIntegerGraph : INode<int>
    {
        private int _min;
        private int _max;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearIntegerGraph"/> struct.
        /// </summary>
        /// <param name="min">The minimum value in the graph.</param>
        /// <param name="max">The maximum value in the graph.</param>
        public LinearIntegerGraph(int min, int max)
        {
            _min = min;
            _max = max;
        }

        /// <inheritdoc/>
        public bool CheckEdge(int node1, int node2)
        {
            // Make sure nodes are within the bounds.
            if (node1 < _min) return false;
            if (node2 > _max) return false;
            
            // The edge exists if the second node is one greater than the first.
            return node2 == node1 + 1;
        }

        /// <inheritdoc/>
        public IEnumerable<int> GetConnectedNodes(int node)
        {
            // Make sure the node is in range
            if (node >= _max) return new int[0];
            if (node < _min) return new int[0];

            // Return the next node
            return new[] { node + 1 };
        }
    }
}
