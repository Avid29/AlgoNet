// Adam Dernis © 2022

using System.Numerics;

namespace AlgoNet.Tests.Data
{
    public static class ExplicitSets
    {
        public static ExplicitDataSet<double> Double_Test1 = new ExplicitDataSet<double>(
            "Double test 1",
            new double[]
            {
                0,
                1,
                8,
                10,
                12,
                22,
                24,
            });
        
        public static ExplicitDataSet<Vector2> Vector2_Test1 = new ExplicitDataSet<Vector2>(
            "Vector2 Test 1",
            new Vector2[]
            {
                new Vector2(0, 2),
                new Vector2(1, 1),
                new Vector2(2, 0),
                new Vector2(7, 5),
                new Vector2(5, 7),
                new Vector2(6, 6),
            });
    }
}
