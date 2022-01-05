// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MS = AlgoNet.Clustering.MeanShift;
using WMS = AlgoNet.Clustering.WeightedMeanShift;

namespace AlgoNet.Tests.Clustering.MeanShift
{
    public class MSTest<T, TShape, TKernel> : IMSTest
        where T : unmanaged, IEquatable<T>
        where TShape : struct, IGeometricPoint<T>
        where TKernel : struct, IKernel
    {
        public const double ACCEPTED_ERROR = .000001;

        public MSTest(DataSet<T> dataSet, TKernel kernel, TShape shape = default)
        {
            DataSet = dataSet;
            Kernel = kernel;
            Shape = shape;
        }

        public string Name => "MeanShift Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public TKernel Kernel { get; }

        public TShape Shape { get; }

        public void Run()
        {
            MS.Cluster<T, TShape, TKernel>(Data, Kernel, Shape);
        }

        public void RunWeightedCompare()
        {
            var basis = MS.Cluster<T, TShape, TKernel>(Data, Kernel, Shape);
            var actual = WMS.Cluster<T, TShape, TKernel>(Data, Kernel, Shape);

            Assert.AreEqual(
                basis.Count,
                actual.Count,
                $"Failed on test \"{Name}\" where {basis.Count} clusters were expected but {actual.Count} clusters were found.");

            for (int i = 0; i < basis.Count; i++)
            {
                Assert.AreEqual(
                    basis[i].Weight,
                    actual[i].Weight,
                    $"Failed on test \"{Name}\" because cluster {i} expected {basis[i].Weight} items and had {actual[i].Weight} items.");

                double distance = Shape.FindDistanceSquared(basis[i].Centroid, actual[i].Centroid);
                Assert.IsTrue(
                     distance <= ACCEPTED_ERROR,
                    $"Failed on test \"{Name}\" because cluster {i} expected was {distance} different from the expected value, which is greater than {ACCEPTED_ERROR}.");
            }
        }
    }
}
