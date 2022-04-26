// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Linq;
using KM = AlgoNet.Clustering.KMeans;

namespace AlgoNet.Tests.Clustering.KMeans
{
    public class KMeansTest<T, TShape> : IKMeansTest
        where T : unmanaged
        where TShape : struct, IGeometricSpace<T>
    {
        public KMeansTest(DataSet<T> dataSet, KMeansConfig<T, TShape> config, TShape shape = default)
        {
            DataSet = dataSet;
            Config = config;
            Shape = shape;
        }

        public string Name => "KMeans Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public KMeansConfig<T, TShape> Config { get; }

        public TShape Shape { get; }

        public void Run()
        {
            var results = KM.Cluster(Data, Config, Shape);
            Validate.CentroidValidate<T, KMeansCluster<T, TShape>, TShape>(results.ToList(), Shape);
        }
    }
}
