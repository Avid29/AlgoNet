// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS = AlgoNet.Sorting.SelectionSort;

namespace AlgoNet.Tests.Sorting.SelectionSort
{
    [TestClass]
    public class SelectionSortTests
    {
        private static void RunTest(int[] data)
        {
            SS.Sort(data);
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
    }
}
