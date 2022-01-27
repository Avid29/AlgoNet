// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BS = AlgoNet.Sorting.BubbleSort;

namespace AlgoNet.Tests.Sorting
{
    [TestClass]
    public class BubbleSortTests
    {
        private static void RunTest(int[] data)
        {
            BS.Sort(data);
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
