// Adam Dernis © 2022

using Microsoft.VisualStudio.TestTools.UnitTesting;
using BS = AlgoNet.Sorting.BubbleSort;

namespace AlgoNet.Tests.Sorting
{
    [TestClass]
    public class BubbleSortTests : SortingTests
    {
        protected override void RunTest(int[] data)
        {
            BS.Sort(data);
            Common.VerifySorted(data);
        }
    }
}
