// Adam Dernis © 2021

using System;

namespace AlgoNet.Tests.Gradients.Easing
{
    public class CubicEase : EasingBase
    {
        public CubicEase(EasingMode mode)
        {
            EasingMode = mode;
            switch (mode)
            {
                case EasingMode.EaseIn:
                    EaseFunc = CubicEaseIn;
                    break;
                case EasingMode.EaseOut:
                    EaseFunc = CubicEaseOut;
                    break;
                case EasingMode.EaseInOut:
                default:
                    EaseFunc = CubicEaseInOut;
                    break;
            }
        }

        public EasingMode EasingMode { get; }

        private Func<double, double> EaseFunc { get; }

        public override double Ease(double pos)
        {
            return EaseFunc(pos);
        }

        private double CubicEaseIn(double x)
        {
            return x * x * x;
        }

        private double CubicEaseOut(double x)
        {
            x--;
            return x * x * x + 1;
        }

        private double CubicEaseInOut(double x)
        {
            if (x < .5)
                return 4 * x * x * x;

            x *= 2;
            x -= 2;
            return (x * x * x / 2) + 1;
        }
    }
}
