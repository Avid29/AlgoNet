// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.DataSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS = AlgoNet.Sorting.CountingSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class CountingSortTests
    {
        private static void RunTest(int[] data)
        {
            data = CS.Sort(data, x => x, data.Length);
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
