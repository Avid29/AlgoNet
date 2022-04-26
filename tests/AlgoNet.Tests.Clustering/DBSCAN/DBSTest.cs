// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using DBS = AlgoNet.Clustering.DBSCAN;

namespace AlgoNet.Tests.Clustering.DBSCAN
{
    public class DBSTest<T, TShape> : IDBSTest
        where T : unmanaged
        where TShape : struct, IDistanceSpace<T>
    {
        public DBSTest(DataSet<T> dataSet, DBSConfig<T, TShape> config, TShape shape = default)
        {
            DataSet = dataSet;
            Config = config;
            Shape = shape;
        }

        public string Name => "DBSCAN Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public DBSConfig<T, TShape> Config { get; }

        public TShape Shape { get; }

        public void Run()
        {
            DBS.Cluster(Data, Config, Shape);
        }
    }
}
