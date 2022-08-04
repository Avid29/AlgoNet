﻿// Adam Dernis © 2022

using AlgoNet.Clustering;
using AlgoNet.Clustering.Kernels;
using AlgoNet.Tests.Data;
using System.Numerics;

namespace AlgoNet.Tests.Clustering.MeanShift.Tests
{
    public static class GradientTests
    {
        public static MSTest<double, int, DoubleShape, GaussianKernel> Gaussian_Linear1D_101 =
            new(GradientSets.Linear1D_101, new GaussianKernel(.1));

        public static MSTest<double, int, DoubleShape, UniformKernel> Uniform_Linear1D_101 =
            new(GradientSets.Linear1D_101, new UniformKernel(.1));


        public static MSTest<double, int, DoubleShape, GaussianKernel> Gaussian_QuadraticEaseIn1D_401 =
            new(GradientSets.QuadraticEaseIn1D_401, new GaussianKernel(.05));

        public static MSTest<double, int, DoubleShape, UniformKernel> Uniform_QuadraticEaseIn1D_401 =
            new(GradientSets.QuadraticEaseIn1D_401, new UniformKernel(.05));


        public static MSTest<Vector2, (int, int), Vector2Shape, GaussianKernel> Gaussian_Linear2D_11x11 =
            new(GradientSets.Linear2D_11x11, new GaussianKernel(.1));

        public static MSTest<Vector2, (int, int), Vector2Shape, UniformKernel> Uniform_Linear2D_11x11 =
            new(GradientSets.Linear2D_11x11, new UniformKernel(.1));


        public static MSTest<Vector2, (int, int), Vector2Shape, GaussianKernel> Gaussian_QuadraticEaseInOut2D_21x21 =
            new(GradientSets.QuadraticEaseInOut2D_21x21, new GaussianKernel(.05));

        public static MSTest<Vector2, (int, int), Vector2Shape, UniformKernel> Uniform_QuadraticEaseInOut2D_21x21 =
            new(GradientSets.QuadraticEaseInOut2D_21x21, new UniformKernel(.05));


        public static IMSTest[] All = new IMSTest[]
        {
            Gaussian_Linear1D_101,
            Uniform_Linear1D_101,
            Gaussian_QuadraticEaseIn1D_401,
            Uniform_QuadraticEaseIn1D_401,
            Gaussian_Linear2D_11x11,
            Uniform_Linear2D_11x11,
            Gaussian_QuadraticEaseInOut2D_21x21,
            Uniform_QuadraticEaseInOut2D_21x21,
        };
    }
}
