// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.MeanShiftPP
{
    public static class ExplicitTests
    {
        public static MSPPTest<double, int, DoubleShape, GaussianKernel> Gaussian_DoubleTest1 =
            new(ExplicitSets.Double_Test1, new GaussianKernel(5));

        public static MSPPTest<double, int, DoubleShape, GaussianKernel> Uniform_DoubleTest1 =
            new(ExplicitSets.Double_Test1, new GaussianKernel(5));

        public static IMSPPTest[] AllDouble =
        {
            Gaussian_DoubleTest1,
            Uniform_DoubleTest1,
        };


        public static MSPPTest<Vector2, (int, int), Vector2Shape, GaussianKernel> Gaussian_Vector2Test1 =
            new(ExplicitSets.Vector2_Test1, new GaussianKernel(5));

        public static MSPPTest<Vector2, (int, int), Vector2Shape, UniformKernel> Uniform_Vector2Test1 =
            new(ExplicitSets.Vector2_Test1, new UniformKernel(5));

        public static IMSPPTest[] AllVector =
        {
            Gaussian_Vector2Test1,
            Uniform_Vector2Test1,
        };

        public static IMSPPTest[] All =
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
