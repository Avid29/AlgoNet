﻿// Adam Dernis © 2022

using AlgoNet.Graphs;
using AlgoNet.Tests.Graph.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AlgoNet.Tests.Graph.Paths
{
    [TestClass]
    public class DijkstraTests
    {
        private static void RunTest<TNode>(int[] data, int source, int target, TNode shape = default)
            where TNode : struct, IWeightedNode<int>
        {
            var result = Dijkstras.Path(data, source, target, out Dictionary<int, double> dists, shape);

            Assert.IsNotNull(result);

            for (int i = 0; i < result.Count - 1; i++)
            {
                if (i == 0)
                    Assert.AreEqual(source, result[i]);
                else if (i == result.Count - 2)
                    Assert.AreEqual(target, result[i + 1]);

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
            LinearIntegerGraph shape = new LinearIntegerGraph(0, size - 1);
            RunTest(data, 0, size - 1, shape);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        public void RunBinaryTreeTests(int size)
        {
            int[] data = Enumerable.Range(0, size).ToArray();
            BinaryTreeGraph shape = new BinaryTreeGraph(size - 1, false);
            RunTest(data, 0, size - 1, shape);
        }
    }
}
