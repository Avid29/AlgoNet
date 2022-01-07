// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Tests.Data;
using AlgoNet.Tests.Shapes;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.KMeans
{
    public static class GradientTests
    {
        public static KMeansTest<double, DoubleShape> Linear1D_101 =
            new KMeansTest<double, DoubleShape>(GradientSets.Linear1D_101,
                new KMeansConfig<double, DoubleShape>(3));

        public static KMeansTest<double, DoubleShape> QuadraticEaseIn1D_401 =
            new KMeansTest<double, DoubleShape>(GradientSets.QuadraticEaseIn1D_401,
                new KMeansConfig<double, DoubleShape>(3));

        public static KMeansTest<Vector2, Vector2Shape> Linear2D_11x11 =
            new KMeansTest<Vector2, Vector2Shape>(GradientSets.Linear2D_11x11,
                new KMeansConfig<Vector2, Vector2Shape>(4));

        public static KMeansTest<Vector2, Vector2Shape> QuadraticEaseInOut2D_21x21 =
            new KMeansTest<Vector2, Vector2Shape>(GradientSets.QuadraticEaseInOut2D_21x21,
                new KMeansConfig<Vector2, Vector2Shape>(4));

        public static IKMeansTest[] All = new IKMeansTest[]
        {
            Linear1D_101,
            QuadraticEaseIn1D_401,
            Linear2D_11x11,
            QuadraticEaseInOut2D_21x21,
        };
    }
}
