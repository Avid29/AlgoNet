// Adam Dernis © 2022

namespace AlgoNet.Tests.Data
{
    public class ExplicitDataSet<T> : DataSet<T>
    {
        public ExplicitDataSet(string name, T[] data) :
            base(name)
        {
            Data = data;
        }

        public override string Type => "Explicit";

        public override T[] Data { get; }
    }
}
