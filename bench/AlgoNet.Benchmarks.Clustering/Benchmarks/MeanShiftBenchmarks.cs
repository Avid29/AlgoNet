// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using BenchmarkDotNet.Attributes;
using ColorExtractor;
using System.Numerics;

namespace AlgoNet.Benchmarks.Clustering.Benchmarks
{
    [MemoryDiagnoser]
    public class MeanShiftBenchmarks
    {
        private Vector3[] colors;

        [Params(.15)]
        public double WindowSize;

        [Params(
            "https://th.bing.com/th/id/OIP.F21MZzafrllhDLK-SZ4jhQHaHW?pid=Api&rs=1",
            "https://f4.bcbits.com/img/a3390257927_10.jpg")]
        public string ImageUrl;

        [Params(1920)]
        public int Quality;

        [IterationSetup]
        public void IterationSetup()
        {
            var image = ImageParser.GetImage(ImageUrl).GetAwaiter().GetResult();

            if (image is null)
                return;

            int detail = (int)Math.Sqrt(Quality);
            colors = ImageParser.SampleImage(ImageParser.GetImageColors(image), detail, detail);
        }

        [Benchmark]
        public void MeanShiftGaussian()
        {
            GaussianKernel kernel = new GaussianKernel(WindowSize);
            MeanShift.ClusterRaw<Vector3, Vector3Shape, GaussianKernel>(colors, kernel);
        }

        [Benchmark]
        public void MeanShiftGaussianGPU()
        {
            GaussianKernel kernel = new GaussianKernel(WindowSize);
            MeanShiftGPU.ClusterRaw<Vector3Shape>(colors, kernel);
        }
    }
}
