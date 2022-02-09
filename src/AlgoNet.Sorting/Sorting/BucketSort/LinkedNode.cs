// Adam Dernis © 2022

namespace AlgoNet.Sorting.Sorting.BucketSort
{
    internal class LinkedNode<T>
    {
        internal LinkedNode(T value)
        {
            Value = value;
        }

        internal T Value { get; }

        internal LinkedNode<T> Previous { get; set; }
    }
}
