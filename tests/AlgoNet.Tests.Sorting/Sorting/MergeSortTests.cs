// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MES = AlgoNet.Sorting.MergeSort;

namespace AlgoNet.Tests.Sorting.Sorting
{
    [TestClass]
    public class MergeSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            MES.Sort(data);
            Common.VerifySorted(data);
        }
    }
}
