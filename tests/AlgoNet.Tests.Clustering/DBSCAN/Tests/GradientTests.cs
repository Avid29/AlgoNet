// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.DBSCAN.Tests
{
    public static class GradientTests
    {
        public static DBSTest<double, DoubleShape> Linear1D_101 =
            new DBSTest<double, DoubleShape>(GradientSets.Linear1D_101,
                new DBSConfig<double, DoubleShape>(.01, 2, true));

        public static DBSTest<double, DoubleShape> QuadraticEaseIn1D_401 =
            new DBSTest<double, DoubleShape>(GradientSets.QuadraticEaseIn1D_401,
                new DBSConfig<double, DoubleShape>(.05, 2, true));

        
        public static DBSTest<Vector2, Vector2Shape> Linear2D_11x11 =
            new DBSTest<Vector2, Vector2Shape>(GradientSets.Linear2D_11x11,
                new DBSConfig<Vector2, Vector2Shape>(.2, 4, true));

        public static DBSTest<Vector2, Vector2Shape> QuadraticEaseInOut2D_21x21 =
            new DBSTest<Vector2, Vector2Shape>(GradientSets.QuadraticEaseInOut2D_21x21,
                new DBSConfig<Vector2, Vector2Shape>(.5, 4, true));


        public static IDBSTest[] All = new IDBSTest[]
        {
            Linear1D_101,
            QuadraticEaseIn1D_401,
            Linear2D_11x11,
            QuadraticEaseInOut2D_21x21,
        };
    }
}
