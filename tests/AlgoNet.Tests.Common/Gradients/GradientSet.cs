// Adam Dernis © 2021

using AlgoNet.Tests.Data;
using AlgoNet.Tests.Gradients.Shape;

namespace AlgoNet.Tests.Gradients
{
    public class GradientSet<T, TShape> : DataSet<T>
        where T : unmanaged
        where TShape : struct, IGradient<T>
    {
        private DimensionSpecs[] _specs;

        public GradientSet(string name, DimensionSpecs[] specs) :
            base(name, null)
        {
            _specs = specs;
        }

        public override string Type => "Gradient";

        public override T[] Data => GradientGenerator.Generate<T, TShape>(_specs);
    }
}
