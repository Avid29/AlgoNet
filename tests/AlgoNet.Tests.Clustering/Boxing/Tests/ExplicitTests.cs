// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.Boxing
{
    public static class ExplicitTests
    {
        public static BoxingTest<double, DoubleShape> DoubleTest1 =
            new BoxingTest<double, DoubleShape>(ExplicitSets.Double_Test1, 5);

        public static BoxingTest<Vector2, Vector2Shape> Vector2Test1 =
            new BoxingTest<Vector2, Vector2Shape>(ExplicitSets.Vector2_Test1, 5);

        public static IBoxingTest[] All = new IBoxingTest[]
        {
            DoubleTest1,
            Vector2Test1,
        };
    }
}
