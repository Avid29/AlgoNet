// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using KM = AlgoNet.Clustering.KMeans;

namespace AlgoNet.Tests.Clustering.KMeans
{
    public class KMeansTest<T, TShape> : IKMeansTest
        where T : unmanaged
        where TShape : struct, IGeometricPoint<T>
    {
        public KMeansTest(DataSet<T> dataSet, KMeansConfig<T, TShape> config, TShape shape = default)
        {
            DataSet = dataSet;
            Config = config;
            Shape = shape;
        }

        public string Name => "DBSCAN Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public KMeansConfig<T, TShape> Config { get; }

        public TShape Shape { get; }

        public void Run()
        {
            KM.Cluster(Data, Config, Shape);
        }
    }
}
