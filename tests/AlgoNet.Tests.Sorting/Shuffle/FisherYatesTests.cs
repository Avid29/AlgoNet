// Adam Dernis © 2022

using AlgoNet.Sorting.Shuffle;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AlgoNet.Tests.Sorting.Shuffle
{
    [TestClass]
    public class FisherYatesTests
    {
        private static void RunTest(int[] data)
        {
            int[] clone = new int[data.Length];
            Array.Copy(data, clone, data.Length);

            FisherYates.Shuffle(data);

            Assert.IsFalse(Common.AreEquivilent(data, clone));
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(100)]
        public void Test(int size)
        {
            RunTest(Enumerable.Range(0, size).ToArray());
        }
    }
}
