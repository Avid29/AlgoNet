// Adam Dernis © 2022

using AlgoNet.Tests.Sorting.Select.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QS3 = AlgoNet.Sorting.Select.Quick3Select;

namespace AlgoNet.Tests.Sorting.Select
{
    [TestClass]
    public class Quick3SelectTests : SelectionTests
    {
        protected override void RunTest(int[] data, int k)
        {
            int kth = QS3.Select(data, k);
            Common.VerifyPartition(data, k, kth);
        }
    }
}
