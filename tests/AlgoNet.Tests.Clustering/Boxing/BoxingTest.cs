// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System;
using Box = AlgoNet.Clustering.Boxing;

namespace AlgoNet.Tests.Clustering.Boxing
{
    public class BoxingTest<T, TCell, TShape> : IBoxingTest
        where T : unmanaged
        where TCell : unmanaged, IEquatable<TCell>
        where TShape : struct, IGridSpace<T, TCell>, IAverageSpace<T>
    {
        public BoxingTest(DataSet<T> dataSet, double window, TShape shape = default)
        {
            DataSet = dataSet;
            Window = window;
            Shape = shape;
        }

        public string Name => "Boxing Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public double Window { get; }

        public TShape Shape { get; }

        public void Run()
        {
            Box.Cluster<T, TCell, TShape>(Data, Window, Shape);
        }
    }
}
