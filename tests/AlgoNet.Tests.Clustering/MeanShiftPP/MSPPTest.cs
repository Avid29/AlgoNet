// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using System;
using MSPPC = AlgoNet.Clustering.MeanShiftPP;

namespace AlgoNet.Tests.Clustering.MeanShiftPP
{
    public class MSPPTest<T, TCell, TShape, TKernel> : IMSPPTest
        where T : unmanaged, IEquatable<T>
        where TCell : unmanaged, IEquatable<TCell>
        where TShape : struct, IGeometricSpace<T, TCell>
        where TKernel : struct, IKernel
    {
        public const double ACCEPTED_ERROR = .000001;

        public MSPPTest(DataSet<T> dataSet, TKernel kernel, TShape shape = default)
        {
            DataSet = dataSet;
            Kernel = kernel;
            Shape = shape;
        }

        public string Name => "MeanShift++ Test: " + DataSet.Name;

        public DataSet<T> DataSet { get; }

        public T[] Data => DataSet.Data;

        public TKernel Kernel { get; }

        public TShape Shape { get; }

        public void Run()
        {
            MSPPC.Cluster<T, TCell, TShape>(Data, Data, Kernel.WindowSize, Shape);
        }
    }
}
