// Adam Dernis © 2021

using AlgoNet.Tests.Gradients;
using AlgoNet.Tests.Gradients.Easing;
using AlgoNet.Tests.Gradients.Shape;
using System.Numerics;

namespace AlgoNet.Tests.Data
{
    public static class GradientSets
    {
        public static GradientSet<double, DoubleGradientShape> Linear1D_101 = new GradientSet<double, DoubleGradientShape>(
            "1D Linear Gradient 101",
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 1, 101, new LinearEase()),
            });

        public static GradientSet<double, DoubleGradientShape> QuadraticEaseIn1D_401 = new GradientSet<double, DoubleGradientShape>(
            "1D Quadratic EaseIn Gradient 401",
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 1, 401, new QuadraticEase(EasingMode.EaseIn)),
            });

        public static GradientSet<Vector2, Vector2GradientShape> Linear2D_11x11 =
            new GradientSet<Vector2, Vector2GradientShape>(
            "2D Linear Gradient 11x11",
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 1, 11, new LinearEase()),
                new DimensionSpecs(0, 1, 11, new LinearEase()),
            });

        public static GradientSet<Vector2, Vector2GradientShape> QuadraticEaseInOut2D_21x21 =
            new GradientSet<Vector2, Vector2GradientShape>(
            "2D Quadratic EaseInOut Gradient 21x21",
            new DimensionSpecs[]
            {
                new DimensionSpecs(0, 1, 21, new QuadraticEase(EasingMode.EaseInOut)),
                new DimensionSpecs(0, 1, 21, new QuadraticEase(EasingMode.EaseInOut)),
            });

        public static GradientSet<double, DoubleGradientShape>[] All1D = new GradientSet<double, DoubleGradientShape>[]
        {
            Linear1D_101,
            QuadraticEaseIn1D_401,
        };

        public static GradientSet<Vector2, Vector2GradientShape>[] All2D = new GradientSet<Vector2, Vector2GradientShape>[]
        {
            Linear2D_11x11,
            QuadraticEaseInOut2D_21x21,
        };
    }
}
