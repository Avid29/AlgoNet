// Adam Dernis © 2022

using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using AlgoNet.Tests.Shapes;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.MeanShift.Tests
{
    public static class Vector2Tests
    {
        public static MSTest<Vector2, Vector2Shape, GaussianKernel> Gaussian_Vector2Test1 =
            new MSTest<Vector2, Vector2Shape, GaussianKernel>(Vector2Sets.Vector2Test1, new GaussianKernel(5));

        public static MSTest<Vector2, Vector2Shape, UniformKernel> Uniform_Vector2Test1 =
            new MSTest<Vector2, Vector2Shape, UniformKernel>(Vector2Sets.Vector2Test1, new UniformKernel(5));

        public static IMSTest[] All = new IMSTest[]
        {
            Gaussian_Vector2Test1,
            Uniform_Vector2Test1,
        };
    }
}
