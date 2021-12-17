// Adam Dernis © 2021

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Shapes;
using AlgoNet.Tests.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using MS = AlgoNet.Clustering.MeanShift;


namespace AlgoNet.Tests.Clustering.MeanShift
{
    [TestClass]
    public class MeanShiftTests
    {
        private void RunTest<T, TShape>(Test<T> test)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
        {
            GaussianKernel kernel = new GaussianKernel(test.Bandwidth);

            try
            {
                var expected = MS.Cluster<T, TShape, GaussianKernel>(test.Input, test.Input, kernel);
            }
            catch (Exception e)
            {
                throw new Exception($"Test {test.Name} failed.", e);
            }
        }

        [TestMethod]
        public void Explicit()
        {
            foreach (var test in DoubleTests.All)
            {
                RunTest<double, DoubleShape>(test);
            }


            foreach (var test in Vector2Tests.All)
            {
                RunTest<Vector2, Vector2Shape>(test);
            }
        }

        [TestMethod]
        public void Gradients()
        {
            foreach (var test in GradientTests.All1D)
            {
                RunTest<double, DoubleShape>(test);
            }

            foreach (var test in GradientTests.All2D)
            {
                RunTest<Vector2, Vector2Shape>(test);
            }
        }
    }
}
