// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.Boxing
{
    public static class ExplicitTests
    {
        public static BoxingTest<double, int, DoubleShape> DoubleTest1 =
            new(ExplicitSets.Double_Test1, 5);

        public static BoxingTest<Vector2, (int, int), Vector2Shape> Vector2Test1 =
            new(ExplicitSets.Vector2_Test1, 5);

        public static IBoxingTest[] All =
        {
            DoubleTest1,
            Vector2Test1,
        };
    }
}
