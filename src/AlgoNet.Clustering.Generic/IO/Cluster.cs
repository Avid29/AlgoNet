// Adam Dernis © 2021

using System;

namespace AlgoNet.Clustering.Generic
{
    /// <summary>
    /// The base class for clusters.
    /// </summary>
    public abstract class Cluster<T, TShape, TDistance>
        where T : unmanaged
        where TShape : struct, IMetricPoint<T, TDistance>
        where TDistance : IFloatingPoint<TDistance>
    {
    }
}
