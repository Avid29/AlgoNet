// Adam Dernis © 2022

using AlgoNet.Tests.Gradients.Shapes;

namespace AlgoNet.Tests.Data.Gradients.Shapes
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
