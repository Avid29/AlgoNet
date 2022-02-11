// Adam Dernis © 2021

namespace AlgoNet.Tests.Data
{
    public abstract class DataSet<T>
    {
        public DataSet(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract string Type { get; }

        public abstract T[] Data { get; }
    }
}
