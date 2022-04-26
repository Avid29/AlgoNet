// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    public static class ExplicitTests
    {
        public static MSTest<double, DoubleShape, GaussianKernel> Gaussian_DoubleTest1 =
            new MSTest<double, DoubleShape, GaussianKernel>(ExplicitSets.Double_Test1, new GaussianKernel(5));

        public static MSTest<double, DoubleShape, GaussianKernel> Uniform_DoubleTest1 =
            new MSTest<double, DoubleShape, GaussianKernel>(ExplicitSets.Double_Test1, new GaussianKernel(5));

        public static IMSTest[] AllDouble = new IMSTest[]
        {
            Gaussian_DoubleTest1,
            Uniform_DoubleTest1,
        };


        public static MSTest<Vector2, Vector2Shape, GaussianKernel> Gaussian_Vector2Test1 =
            new MSTest<Vector2, Vector2Shape, GaussianKernel>(ExplicitSets.Vector2_Test1, new GaussianKernel(5));

        public static MSTest<Vector2, Vector2Shape, UniformKernel> Uniform_Vector2Test1 =
            new MSTest<Vector2, Vector2Shape, UniformKernel>(ExplicitSets.Vector2_Test1, new UniformKernel(5));

        public static IMSTest[] AllVector = new IMSTest[]
        {
            Gaussian_Vector2Test1,
            Uniform_Vector2Test1,
        };

        public static IMSTest[] All = new IMSTest[]
        {
            // Double
            Gaussian_DoubleTest1,
            Uniform_DoubleTest1,

            // Vector2
            Gaussian_Vector2Test1,
            Uniform_Vector2Test1,
        };
    }
}
