// Adam Dernis © 2022

using System.Collections.Generic;

namespace AlgoNet.Graphs.Interfaces
{
    public interface INode
    {
        List<IEdge> Edges { get; }
    }
}
