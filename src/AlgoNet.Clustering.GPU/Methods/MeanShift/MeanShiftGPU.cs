// Adam Dernis © 2022

using AlgoNet.Clustering.Kernels;
using AlgoNet.Clustering.Shaders;
using ComputeSharp;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AlgoNet.Clustering
{
    public static class MeanShiftGPU
    {
        public static List<MSCluster<Vector3, TShape>> Cluster<TShape>(
            ReadOnlySpan<Vector3> points,
            GaussianKernel kernel)
            where TShape : struct, IDistanceSpace<Vector3>, IWeightedAverageSpace<Vector3>
        {
            return MeanShift.Wrap<Vector3, TShape>(ClusterRaw<TShape>(points, kernel));
        }

        public static (Vector3, int)[] ClusterRaw<TShape>(
            ReadOnlySpan<Vector3> points,
            GaussianKernel kernel)
            where TShape : struct, IDistanceSpace<Vector3>, IWeightedAverageSpace<Vector3>
        {
            var pointBuffer = GraphicsDevice.Default.AllocateReadWriteBuffer(points);
            var weightBuffer = GraphicsDevice.Default.AllocateReadWriteTexture2D<float>(points.Length, points.Length);
            float denominatorBandwidth = (float)kernel.WindowSize * (float)kernel.WindowSize * -2;
            
            GraphicsDevice.Default.For(points.Length,
                new EuclideanVector3MeanShiftShader(
                    pointBuffer,
                    pointBuffer,
                    weightBuffer,
                    denominatorBandwidth));

            var clusteredPoints = pointBuffer.ToArray();
            return MeanShift.PostProcess<Vector3, TShape, GaussianKernel>(clusteredPoints, kernel, default);
        }
    }
}
