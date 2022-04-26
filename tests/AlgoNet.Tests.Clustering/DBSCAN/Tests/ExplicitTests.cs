// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.DBSCAN
{
    public static class ExplicitTests
    {
        public static DBSTest<double, DoubleShape> DoubleTest1 =
            new DBSTest<double, DoubleShape>(ExplicitSets.Double_Test1,
                new DBSConfig<double, DoubleShape>(2, 1, true));

        public static DBSTest<Vector2, Vector2Shape> Vector2Test1 =
            new DBSTest<Vector2, Vector2Shape>(ExplicitSets.Vector2_Test1,
                new DBSConfig<Vector2, Vector2Shape>(2, 2, true));

        public static IDBSTest[] All = new IDBSTest[]
        {
            DoubleTest1,
            Vector2Test1,
        };
    }
}
