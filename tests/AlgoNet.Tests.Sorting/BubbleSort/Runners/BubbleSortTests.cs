// Adam Dernis © 2022

using AlgoNet.Sorting.Methods;
using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.Toolkit.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BS = AlgoNet.Sorting.Methods.BubbleSort;

namespace AlgoNet.Tests.Sorting.BubbleSort.Runners
{
    [TestClass]
    public class BubbleSortTests
    {
        private static void RunTest(int[] data)
        {
            BS.Sort(data);
            VerifySorted(data);
        }

        private static void VerifySorted(int[] sortedArray)
        {
            for (int i = 0; i < sortedArray.Length - 1; i++)
            {
                Assert.IsTrue(sortedArray[i] <= sortedArray[i + 1]);
            }
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
