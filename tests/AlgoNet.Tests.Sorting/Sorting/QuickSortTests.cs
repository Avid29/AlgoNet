// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS = AlgoNet.Sorting.QuickSort;

namespace AlgoNet.Tests.Sorting.Methods
{
    [TestClass]
    public class QuickSortTests
    {
        private static void RunTest(int[] data)
        {
            QS.Sort(data);
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
