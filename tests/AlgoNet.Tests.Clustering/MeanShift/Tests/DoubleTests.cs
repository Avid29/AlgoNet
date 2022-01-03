// Adam Dernis © 2022

using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using AlgoNet.Tests.Shapes;

namespace AlgoNet.Tests.Clustering.MeanShift.Tests
{
    public static class DoubleTests
    {
        public static MSTest<double, DoubleShape, GaussianKernel> GaussianTest1 =
            new MSTest<double, DoubleShape, GaussianKernel>(DoubleSets.Test1, new GaussianKernel(5));

        public static IMSTest[] All = new IMSTest[]
        {
            GaussianTest1,
        };
    }
}
