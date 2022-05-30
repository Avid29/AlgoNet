// Adam Dernis © 2021

namespace AlgoNet.Tests.Gradients.Shapes
{
    public interface IGradient<T>
    {
        int N { get; }

        T For(double[] coords);
    }
}
