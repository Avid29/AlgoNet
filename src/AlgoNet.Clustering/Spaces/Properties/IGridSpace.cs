// Adam Dernis © 2022

namespace AlgoNet.Clustering
{
    public interface IGridSpace<T, TCell> : ISpace<T>
    {
        TCell GetCell(T value, double detail);
    }
}
