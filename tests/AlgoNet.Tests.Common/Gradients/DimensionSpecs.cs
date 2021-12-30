// Adam Dernis © 2021

using AlgoNet.Tests.Gradients.Easing;

namespace AlgoNet.Tests.Gradients
{
    public struct DimensionSpecs
    {
        public DimensionSpecs(double start, double end, int steps, EasingBase ease)
        {
            Start = start;
            End = end;
            Steps = steps;
            Ease = ease;
        }

        public double Start { get; }

        public double End { get; }

        public int Steps { get; }

        public EasingBase Ease { get; }
    }
}
