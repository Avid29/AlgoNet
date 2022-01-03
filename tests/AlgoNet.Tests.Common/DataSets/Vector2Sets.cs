// Adam Dernis © 2021

using System.Numerics;

namespace AlgoNet.Tests.Data
{
    public static class Vector2Sets
    {
        public static DataSet<Vector2> Vector2Test1 = new DataSet<Vector2>(
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

        public static DataSet<Vector2>[] All = new DataSet<Vector2>[]
        {
            Vector2Test1,
        };
    }
}
