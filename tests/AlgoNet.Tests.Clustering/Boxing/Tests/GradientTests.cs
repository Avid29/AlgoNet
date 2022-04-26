// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.Boxing.Tests
{
    public static class GradientTests
    {
        public static BoxingTest<double, DoubleShape> Linear1D_101 =
            new BoxingTest<double, DoubleShape>(GradientSets.Linear1D_101, .1);

        public static BoxingTest<double, DoubleShape> QuadraticEaseIn1D_401 =
            new BoxingTest<double, DoubleShape>(GradientSets.QuadraticEaseIn1D_401, .5);

        
        public static BoxingTest<Vector2, Vector2Shape> Linear2D_11x11 =
            new BoxingTest<Vector2, Vector2Shape>(GradientSets.Linear2D_11x11, 3);

        public static BoxingTest<Vector2, Vector2Shape> QuadraticEaseInOut2D_21x21 =
            new BoxingTest<Vector2, Vector2Shape>(GradientSets.QuadraticEaseInOut2D_21x21, .5);


        public static IBoxingTest[] All = new IBoxingTest[]
        {
            Linear1D_101,
            QuadraticEaseIn1D_401,
            Linear2D_11x11,
            QuadraticEaseInOut2D_21x21,
        };
    }
}
