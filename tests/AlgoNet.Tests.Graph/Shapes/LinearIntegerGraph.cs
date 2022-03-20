// Adam Dernis © 2022

using AlgoNet.Graphs;
using System.Collections.Generic;

namespace AlgoNet.Tests.Graph.Shapes
{
    /// <summary>
    /// A shape for treating integers as a linear graph.
    /// </summary>
    public struct LinearIntegerGraph : IWeightedNode<int>
    {
        private int _min;
        private int _max;
        private bool _cyclic;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearIntegerGraph"/> struct.
        /// </summary>
        /// <param name="min">The minimum value in the graph.</param>
        /// <param name="max">The maximum value in the graph.</param>
        public LinearIntegerGraph(int min, int max, bool cyclic = false)
        {
            _min = min;
            _max = max;
            _cyclic = cyclic;
        }

        /// <inheritdoc/>
        public bool CheckEdge(int node1, int node2)
        {
            // Make sure nodes are within the bounds.
            if (node1 < _min) return false;
            if (node2 > _max) return false;
            
            // The edge exists if the second node is one greater than the first.
            // Or if cyclic max to min.
            return node2 == node1 + 1 || (_cyclic && node1 == _max && node2 == _min);
        }

        /// <inheritdoc/>
        public IEnumerable<int> GetConnectedNodes(int node)
        {
            // Make sure the node is in range
            if (_cyclic && node == _max) return new[] { _min };
            if (node >= _max) return new int[0];
            if (node < _min) return new int[0];

            // Return the next node
            return new[] { node + 1 };
        }

        /// <inheritdoc/>
        public double GetEdgeLength(int node1, int node2)
        {
            return CheckEdge(node1, node2) ? 1 : 0;
        }
    }
}
