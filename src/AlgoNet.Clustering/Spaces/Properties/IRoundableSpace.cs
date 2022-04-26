// Adam Dernis © 2022

namespace AlgoNet.Clustering.Spaces.Properties
{
    public interface IRoundableSpace<T> : ISpace<T>
    {
        T Round(T value, double detail);
    }
}
