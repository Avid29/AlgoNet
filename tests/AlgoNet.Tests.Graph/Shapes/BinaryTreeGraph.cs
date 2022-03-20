// Adam Dernis © 2022

using AlgoNet.Graphs;
using System.Collections.Generic;

namespace AlgoNet.Tests.Graph.Shapes
{
    public struct BinaryTreeGraph : IWeightedNode<int>
    {
        private int _max;
        private bool _directional;

        public BinaryTreeGraph(int max, bool directional = true)
        {
            _max = max;
            _directional = directional;
        }

        public bool CheckEdge(int node1, int node2)
        {
            if (node1 < 0 || node1 > _max ||
                node2 < 0 || node2 > _max)
                return false;

            int parent = (node1 - 1) / 2;
            int left_child = node1 * 2 + 1;
            int right_child = node1 * 2 + 2;

            return node2 == left_child || node2 == right_child || (!_directional && node2 == parent);
        }

        public IEnumerable<int> GetConnectedNodes(int node)
        {
            int parent = (node - 1) / 2;
            int left_child = node * 2 + 1;
            int right_child = node * 2 + 2;

            List<int> connections = new List<int>();
            if (left_child <= _max) connections.Add(left_child);
            if (right_child <= _max) connections.Add(right_child);
            if (!_directional && node != 0) connections.Add(parent);
            return connections;
        }

        public double GetEdgeLength(int node1, int node2)
        {
            return CheckEdge(node1, node2) ? 1 : 0;
        }
    }
}
