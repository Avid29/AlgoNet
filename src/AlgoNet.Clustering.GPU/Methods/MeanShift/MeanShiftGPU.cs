// Adam Dernis © 2022

using AlgoNet.Clustering.Kernels;
using AlgoNet.Clustering.Shaders;
using ComputeSharp;
using System;
using System.Numerics;

namespace AlgoNet.Clustering
{
    public static class MeanShiftGPU
    {
        public static (Vector3, int)[] ClusterRaw<TShape>(
            ReadOnlySpan<Vector3> points,
            GaussianKernel kernel)
            where TShape : struct, IGeometricPoint<Vector3>
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
