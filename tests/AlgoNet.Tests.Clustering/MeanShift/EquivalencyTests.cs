// Adam Dernis © 2021

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Shapes;
using AlgoNet.Tests.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
using MS = AlgoNet.Clustering.MeanShift;
using WMS = AlgoNet.Clustering.WeightedMeanShift;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    [TestClass]
    public class EquivalencyTests
    {
        public const double ACCEPTED_ERROR = .000001;

        private static void CompareResults<T, TShape>(
            Test<T> test,
            List<MSCluster<T, TShape>> expected,
            List<MSCluster<T, TShape>> actual,
            double error = ACCEPTED_ERROR,
            TShape shape = default)
            where T : unmanaged
            where TShape : struct, IGeometricPoint<T>
        {
            Assert.AreEqual(
                expected.Count,
                actual.Count,
                $"Failed on test \"{test.Name}\" where {expected.Count} clusters were expected but {actual.Count} clusters were found.");

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(
                    expected[i].Weight,
                    actual[i].Weight,
                    $"Failed on test \"{test.Name}\" because cluster {i} expected {expected[i].Weight} items and had {actual[i].Weight} items.");

                double distance = shape.FindDistanceSquared(expected[i].Centroid, actual[i].Centroid);
                Assert.IsTrue(
                     distance <= error,
                    $"Failed on test \"{test.Name}\" because cluster {i} expected was {distance} different from the expected value, which is greater than {error}.");
            }
        }

        private static void RunWeightedTest<T, TShape>(Test<T> test)
            where T : unmanaged, IEquatable<T>
            where TShape : struct, IGeometricPoint<T>
        {
            GaussianKernel kernel = new GaussianKernel(test.Bandwidth);
            var expected = MS.Cluster<T, TShape, GaussianKernel>(test.Input, kernel);
            var actual = WMS.Cluster<T, TShape, GaussianKernel>(test.Input, kernel);

            // MeanShift results should be approx equal to Weighted
            CompareResults(test, expected, actual);
        }

        [TestMethod]
        public void WeightedEquivalency()
        {
            foreach (var test in DoubleTests.All)
            {
                RunWeightedTest<double, DoubleShape>(test);
            }

            foreach (var test in Vector2Tests.All)
            {
                RunWeightedTest<Vector2, Vector2Shape>(test);
            }

            foreach (var test in GradientTests.All1D)
            {
                RunWeightedTest<double, DoubleShape>(test);
            }

            foreach (var test in GradientTests.All2D)
            {
                RunWeightedTest<Vector2, Vector2Shape>(test);
            }
        }
    }
}
