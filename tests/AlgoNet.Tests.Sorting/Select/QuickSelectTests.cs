// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.Select.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS = AlgoNet.Sorting.Select.QuickSelect;

namespace AlgoNet.Tests.Sorting.Select
{
    [TestClass]
    public class QuickSelectTests : SelectionTests
    {
        protected override void RunTest(int[] data, int k)
        {
            int kth = QS.Select(data, k);
            Common.VerifyPartition(data, k, kth);
        }
    }
}
