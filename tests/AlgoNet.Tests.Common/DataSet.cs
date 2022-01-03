// Adam Dernis © 2021

namespace AlgoNet.Tests.Data
{
    public class DataSet<T>
    {
        public DataSet(string name)
        {
            Name = name;
        }

        public DataSet(string name, T[] data)
        {
            Name = name;
            Data = data;
        }

        public string Name { get; }

        public virtual string Type { get; } = "Explicit";

        public virtual T[] Data { get; }
    }
}
