// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using Box = AlgoNet.Clustering.Boxing;

namespace AlgoNet.Tests.Clustering.Boxing
{
    public class BoxingTest<T, TShape> : IBoxingTest
        where T : unmanaged
        where TShape : struct, IRoundableSpace<T>, IAverageSpace<T>
    {
        public BoxingTest(DataSet<T> dataSet, double window, TShape shape = default)
        {
            DataSet = dataSet;
            Window = window;
            Shape = shape;
        }

        public string Name => "DBSCAN Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public double Window { get; }

        public TShape Shape { get; }

        public void Run()
        {
            Box.Cluster(Data, Window, Shape);
        }
    }
}
