// Adam Dernis © 2022

using AlgoNet.Graphs.Path.Dijkstras;
using System;
using System.Collections.Generic;

namespace AlgoNet.Graphs
{
    /// <summary>
    /// A static class containing Dijkstra's algorithm methods.
    /// </summary>
    public static class Dijkstras
    {
        /// <summary>
        /// Finds the path from the source to the target.
        /// </summary>
        /// <typeparam name="T">The type of node in the graph.</typeparam>
        /// <typeparam name="TShape">The type of shape to use assessing nodes as a graph.</typeparam>
        /// <param name="graph">The nodes in the graph.</param>
        /// <param name="source">The source node of the path.</param>
        /// <param name="target">The target node of the path, or <see langword="null"/> if finding distance from all points.</param>
        /// <param name="dists">A <see cref="Dictionary{T, double}"/> containing the distance of a given point from the source.</param>
        /// <param name="shape">A shape that can determine edges in the graph.</param>
        /// <returns>The path from the source node to the target node.</returns>
        public static List<T>? Path<T, TShape>(T[] graph, T source, T? target, out Dictionary<T, double> dists, TShape shape = default)
            where T : IEquatable<T>
            where TShape : struct, IWeightedNode<T>
        {
            var context = new DijkstrasContext<T, TShape>(source, target, graph);

            foreach (T node in graph)
            {
                context.Distances.Add(node, double.PositiveInfinity);
                context.Queue.Add(node); 
            }

            context.Distances[context.Source] = 0;

            while (context.Queue.Count > 0)
            {
                T u = DequeMinDistance(context, out double uDistance);

                if (context.Target != null && u.Equals(context.Target))
                    break;

                foreach (T v in shape.GetConnectedNodes(u))
                {
                    double alt = uDistance + shape.GetEdgeLength(u, v);
                    if (alt < context.Distances[v])
                    {
                        context.Distances[v] = alt;
                        context.Previous.GetOrAddValueRef(v) = u;
                    }
                }
            }

            dists = context.Distances;
            return BackTracePath(context);
        }

        private static List<T>? BackTracePath<T, TShape>(DijkstrasContext<T, TShape> context)
            where T : IEquatable<T>
            where TShape : struct, IWeightedNode<T>
        {
            // If there's no target return a null path
            if (context.Target == null) return null;

            List<T> path = new List<T>();
            T? u = context.Target;

            while (context.Previous.TryGetValue(u, out T? prev))
            {
                if (prev == null) break;

                path.Add(u);
                u = prev;
            }

            if (context.Source.Equals(u))
            {
                path.Add(u);
                path.Reverse();
                return path;
            }
            return null;
        }

        private static T DequeMinDistance<T, TShape>(DijkstrasContext<T, TShape> context, out double distance)
            where T : IEquatable<T>
            where TShape : struct, IWeightedNode<T>
        {
            int minIndex = 0;
            T minNode = context.Queue[minIndex];
            distance = context.Distances[minNode];
            for (int i = 0; i < context.Queue.Count; i++)
            {
                T node = context.Queue[i];
                if (context.Distances[node] < distance)
                {
                    minIndex = i;
                    minNode = node;
                    distance = context.Distances[node];
                }
            }
            context.Queue.RemoveAt(minIndex);
            return minNode;
        }
    }
}
