// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS = AlgoNet.Sorting.QuickSort;

namespace AlgoNet.Tests.Sorting.Select
{
    [TestClass]
    public class QuickSelectTests
    {
        private static void RunTest(int[] data, int k)
        {
            int kth = QS.Select(data, k);

            bool isValid = true;
            for (int i = 0; i < k; i++)
                if (data[i] > kth) isValid = false;

            for (int i = k+1; i < data.Length; i++)
                if (data[i] < kth) isValid = false;

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void PresortedTests()
        {
            foreach (var set in PresortedSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }

        [TestMethod]
        public void ReverseOrderTests()
        {
            foreach (var set in ReverseOrderSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }

        [TestMethod]
        public void RandomizedTests()
        {
            foreach (var set in RandomizedSets.All)
            {
                RunTest(set, set.Length / 4);
                RunTest(set, set.Length / 2);
                RunTest(set, 3 * set.Length / 4);
            }
        }
    }
}
