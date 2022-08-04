// Adam Dernis © 2022

using System;
using System.Collections.Generic;

namespace AlgoNet.Clustering
{
    public static class MeanShiftPP
    {
        public static (T, int)[] Cluster<T, TCell, TShape>(
            ReadOnlySpan<T> points,
            ReadOnlySpan<T> field,
            double window,
            TShape shape = default)
        where T : unmanaged, IEquatable<T>
        where TCell : unmanaged, IEquatable<TCell>
        where TShape : struct, IDistanceSpace<T>, IAverageSpace<T>, IWeightedAverageSpace<T>, IGridSpace<T, TCell>
        {
            var cells = Boxing.Cluster<T, TCell, TShape>(field, window, shape);
            var cellMap = new Dictionary<TCell, (T, int)>();

            foreach (var point in cells)
            {
                TCell cell = shape.GetCell(point.Item1, window);
                cellMap.Add(cell, point);
            }
            
            // Points will bed cloned into a modifiable list of clusters
            T[] clusters = new T[points.Length];

            for (int p = 0; p < points.Length; p++)
            {
                TCell cell = shape.GetCell(points[p], window);
                var nCells = shape.GetNeighbors(cell);
                var ns = new (T, double)[nCells.Length + 1];

                int i;
                for (i = 0; i < nCells.Length; i++)
                {
                    var nCell = nCells[i];
                    if (cellMap.ContainsKey(nCell))
                        ns[i] = cellMap[nCell];
                }
                
                if (cellMap.ContainsKey(cell))
                    ns[i] = cellMap[cell];

                clusters[p] = shape.WeightedAverage(ns);
            }

            return MeanShift.PostProcess(clusters, window, shape);
        }
    }
}
