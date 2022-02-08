// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES = AlgoNet.Sorting.MergeSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class MergeSortTests
    {
        private static void RunTest(int[] data)
        {
            MES.Sort(data);
            Common.VerifySorted(data);
        }

        [TestMethod]
        public void PresortedTests()
        {
            foreach (var set in PresortedSets.All)
            {
                RunTest(set);
            }
        }

        [TestMethod]
        public void ReverseOrderTests()
        {
            foreach (var set in ReverseOrderSets.All)
            {
                RunTest(set);
            }
        }

        [TestMethod]
        public void RandomizedTests()
        {
            foreach (var set in RandomizedSets.All)
            {
                RunTest(set);
            }
        }
    }
}
