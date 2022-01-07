// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using AlgoNet.Tests.Shapes;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.KMeans
{
    public static class ExplicitTests
    {
        public static KMeansTest<double, DoubleShape> DoubleTest1 =
            new KMeansTest<double, DoubleShape>(ExplicitSets.Double_Test1,
                new KMeansConfig<double, DoubleShape>(3));

        public static KMeansTest<Vector2, Vector2Shape> Vector2Test1 =
            new KMeansTest<Vector2, Vector2Shape>(ExplicitSets.Vector2_Test1,
                new KMeansConfig<Vector2, Vector2Shape>(2));

        public static IKMeansTest[] All = new IKMeansTest[]
        {
            DoubleTest1,
            Vector2Test1,
        };
    }
}
