// Adam Dernis © 2022

using Microsoft.Collections.Extensions;
using System;
using System.Collections.Generic;

namespace AlgoNet.Graphs.Path.Dijkstras
{
    internal ref struct DijkstrasContext<T, TShape>
        where T : IEquatable<T>
        where TShape : struct, IWeightedNode<T>
    {
        internal DijkstrasContext(T source, T? target, T[] graph)
        {
            Source = source;
            Target = target;
            Graph = graph;
            Queue = new List<T>();
            Previous = new DictionarySlim<T, T?>();
            Distances = new Dictionary<T, double>();
        }

        internal T Source { get; }

        internal T? Target { get; }

        internal T[] Graph { get; }

        internal List<T> Queue { get; }

        internal DictionarySlim<T, T?> Previous { get; }

        internal Dictionary<T, double> Distances { get; }
    }
}
