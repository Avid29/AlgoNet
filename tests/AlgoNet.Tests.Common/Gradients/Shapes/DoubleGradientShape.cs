// Adam Dernis © 2021

namespace AlgoNet.Tests.Gradients.Shapes
{
    public struct DoubleGradientShape : IGradient<double>
    {
        public int N => 1;

        public double For(double[] coords)
        {
            return coords[0];
        }
    }
}
