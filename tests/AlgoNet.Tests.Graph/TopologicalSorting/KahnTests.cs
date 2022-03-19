using AlgoNet.Graphs;
using AlgoNet.Graphs.TopologicalSorting;
using AlgoNet.Tests.Graph.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AlgoNet.Tests.Graph.TopologicalSorting
{
    [TestClass]
    public class KahnTests
    {
        private static void RunTest<TNode>(int[] data, TNode shape = default)
            where TNode : struct, INode<int>
        {
            var result = Kahns.Sort(data, shape);

            Assert.IsNotNull(result);

            for (int i = 0; i < result.Count - 1; i++)
            {
                if (result[i] > result[i + 1])
                    Assert.Fail();
            }
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        public void RunLinearTests(int size)
        {
            int[] data = Enumerable.Range(0, size).ToArray();
            LinearIntegerGraph shape = new LinearIntegerGraph(0, size-1);
            RunTest(data, shape);
        }
    }
}
