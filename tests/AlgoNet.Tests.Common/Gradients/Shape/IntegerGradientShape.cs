// Adam Dernis © 2022

using AlgoNet.Tests.Gradients.Shape;

namespace AlgoNet.Tests.Data.Gradients.Shape
{
    public struct IntegerGradientShape : IGradient<int>
    {
        public int N => 1;

        public int For(double[] coords)
        {
            return (int)coords[0];
        }
    }
}
